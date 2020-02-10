using System;
using System.Linq;
using Gma.System.MouseKeyHook;
using System.Windows.Forms;
using MultiBuffer.WpfApp.Models.Interfaces;
using System.Diagnostics;
using System.Collections.Generic;
using WK.Libraries.SharpClipboardNS;
using WindowsInput;
using WindowsInput.Native;
using System.Threading.Tasks;

namespace MultiBuffer.WpfApp.Models.Controllers
{
    public class InputController : IInputController
    {
        public InputController(IInputSimulator simulator)
        {
            _inputSimulator = simulator;
            var clipboardMonitor = new SharpClipboard();
            clipboardMonitor.ClipboardChanged += ClipboardMonitor_ClipboardChanged;

            Hook.GlobalEvents().KeyDown += InputController_KeyDown;
        }

        /// <summary>
        /// Хранит последовательность нажатых клавиш для копирования
        /// </summary>
        List<Keys> _keysCopyList = new List<Keys>();

        /// <summary>
        /// Хранит последовательность нажатых клавиш для вставки
        /// </summary>
        List<Keys> _keysPasteList = new List<Keys>();

        /// <summary>
        /// Перехватывает нажатие всех клавиш в системе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Нажатая клавиша</param>
        private void InputController_KeyDown(object sender, KeyEventArgs e)
        {
            //ESC 
            if(e.KeyCode == Keys.Escape)
            {
                _keysCopyList.Clear();
                _keysPasteList.Clear();
            }
            //LeftCtrl
            else if ((_keysCopyList.Count == 0 || _keysPasteList.Count == 0) && e.KeyCode == Keys.LControlKey)
            {
                _keysCopyList.Add(e.KeyCode);
                _keysPasteList.Add(e.KeyCode);
                Debug.WriteLine("LeftCtrl");
            }
            //LeftCtrl + C
            else if (_keysCopyList.Count == 1 && _keysCopyList[0] == Keys.LControlKey && e.KeyCode == Keys.C)
            {
                _keysPasteList.Clear();
                _keysCopyList.Add(e.KeyCode);
                Debug.WriteLine("LeftCtrl + C");
            }
            //LeftCtrl + V
            else if(_keysPasteList.Count == 1 && _keysPasteList[0] == Keys.LControlKey && e.KeyCode == Keys.V)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                _keysCopyList.Clear();
                _keysPasteList.Add(e.KeyCode);
                Debug.WriteLine("LeftCtrl + V");
            }
            //LeftCtrl + C + AnyKey
            else if (_keysCopyList.Count == 2 && _keysCopyList[0] == Keys.LControlKey && _keysCopyList[1] == Keys.C)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                _keysCopyList.Add(e.KeyCode);
                Debug.WriteLine("LeftCtrl + C + AnyKey " + e.KeyCode);
            }
            //LeftCtrl + V + AnyKey
            else if (_keysPasteList.Count == 2 && _keysPasteList[0] == Keys.LControlKey && _keysPasteList[1] == Keys.V)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                _keysPasteList.Add(e.KeyCode);
                Debug.WriteLine("LeftCtrl + V + AnyKey " + e.KeyCode);
                _keysPasteList.Clear();
            }
            else
            {
                _keysCopyList.Clear();
                _keysPasteList.Clear();
                Debug.WriteLine("Keys lists was cleared!");
            }
        }

        /// <summary>
        /// Эмулирует нажатие клавиш
        /// </summary>
        private readonly IInputSimulator _inputSimulator;

        /// <summary>
        /// Изменилось значение буфера обмена Windows
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ClipboardMonitor_ClipboardChanged(object sender, SharpClipboard.ClipboardChangedEventArgs e)
        {
            Debug.WriteLine("Clipboard was changed!!!");

            while (_keysCopyList.Count >= 2 && _keysCopyList[0] == Keys.LControlKey && _keysCopyList[1] == Keys.C)
            {
                await Task.Run(() =>
                    {
                        if (_keysCopyList.Count == 3)
                        {
                            if (e.ContentType == SharpClipboard.ContentTypes.Text)
                                CopyKeyPress?.Invoke(this, new InputControllerEventArgs(_keysCopyList[2], (string)e.Content));
                            _keysCopyList.Clear();
                        }
                    }
                );
            }
        }

        /// <summary>
        /// Указывает, что была нажата клавиша вставки
        /// </summary>
        public event EventHandler<InputControllerEventArgs> PasteKeyPress;

        /// <summary>
        /// Указывает, что была нажата клавиша копирования
        /// </summary>
        public event EventHandler<InputControllerEventArgs> CopyKeyPress;
    }
}
