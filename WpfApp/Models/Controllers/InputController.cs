using System;
using Gma.System.MouseKeyHook;
using System.Windows.Forms;
using MultiBuffer.WpfApp.Models.Interfaces;
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
            else if (_keysPasteList.Count == 3 && _keysPasteList[0] == Keys.LControlKey && _keysPasteList[1] == Keys.V)
            {
                _keysPasteList.Clear();
            }
            //LeftCtrl
            else if ((_keysCopyList.Count == 0 || _keysPasteList.Count == 0) && e.KeyCode == Keys.LControlKey)
            {
                _keysCopyList.Add(e.KeyCode);
                _keysPasteList.Add(e.KeyCode);
            }
            //LeftCtrl + C
            else if (_keysCopyList.Count == 1 && _keysCopyList[0] == Keys.LControlKey && e.KeyCode == Keys.C)
            {
                _keysPasteList.Clear();
                _keysCopyList.Add(e.KeyCode);
            }
            //LeftCtrl + V
            else if(_keysPasteList.Count == 1 && _keysPasteList[0] == Keys.LControlKey && e.KeyCode == Keys.V)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                _keysCopyList.Clear();
                _keysPasteList.Add(e.KeyCode);
            }
            //LeftCtrl + C + AnyKey
            else if (_keysCopyList.Count == 2 && _keysCopyList[0] == Keys.LControlKey && _keysCopyList[1] == Keys.C)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                _keysCopyList.Add(e.KeyCode);
            }
            //LeftCtrl + V + AnyKey
            else if (_keysPasteList.Count == 2 && _keysPasteList[0] == Keys.LControlKey && _keysPasteList[1] == Keys.V)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                _keysPasteList.Add(e.KeyCode);
                PasteKeyPress?.Invoke(this, new InputControllerEventArgs(_keysPasteList[2], string.Empty));
            }
            else
            {
                _keysCopyList.Clear();
                _keysPasteList.Clear();
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
            if (_keysPasteList.Count == 3 && _keysPasteList[0] == Keys.LControlKey && _keysPasteList[1] == Keys.V)
            {
                _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LCONTROL);
                _inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_V);
                _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);
                return;
            }

            await Task.Run(() =>
            {
                while (_keysCopyList.Count >= 2 && _keysCopyList[0] == Keys.LControlKey && _keysCopyList[1] == Keys.C)
                {

                    if (_keysCopyList.Count == 3)
                    {
                        if (e.ContentType == SharpClipboard.ContentTypes.Text)
                            CopyKeyPress?.Invoke(this, new InputControllerEventArgs(_keysCopyList[2], (string)e.Content));
                        _keysCopyList.Clear();
                        return;
                    }
                }
            });
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
