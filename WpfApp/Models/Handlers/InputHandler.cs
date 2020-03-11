using System;
using Gma.System.MouseKeyHook;
using System.Windows.Forms;
using MultiBuffer.WpfApp.Models.Interfaces;
using System.Collections.Generic;
using WK.Libraries.SharpClipboardNS;
using WindowsInput;
using WindowsInput.Native;
using System.Threading.Tasks;

namespace MultiBuffer.WpfApp.Models.Handlers
{
    public class InputHandler : IInputHandler
    {
        public InputHandler()
        {
            var clipboardMonitor = new SharpClipboard();
            clipboardMonitor.ClipboardChanged += ClipboardMonitor_ClipboardChanged;

            Hook.GlobalEvents().KeyDown += InputController_KeyDown;
        }

        /// <summary>
        /// Указывает, что была нажата клавиша вставки
        /// </summary>
        public event EventHandler<InputHandlerEventArgs> PasteKeyPress;

        /// <summary>
        /// Указывает, что была нажата клавиша копирования
        /// </summary>
        public event EventHandler<InputHandlerEventArgs> CopyKeyPress;

        /// <summary>
        /// Указывает, что была нажата клавиша отображения окна
        /// </summary>
        public event EventHandler ShowWindowKeyPress;

        /// <summary>
        /// Событие возникает после нажатия клавиш LCtrl + C
        /// После него ожидается нажатие клавиши для вставки в буфер
        /// </summary>
        public event Action CopyIsActive;

        /// <summary>
        /// Событие возникает после нажатия клавиш LCtrl + V
        /// После него ожидается нажатие клавиши для вставки в буфер
        /// </summary>
        public event Action PasteIsActive;

        /// <summary>
        /// Отменяет ожидание клавиши после клавиш вставки/копирования
        /// </summary>
        public event Action CopyPasteCancelled;

        /// <summary>
        /// Хранит последовательность нажатых клавиш для копирования
        /// </summary>
        readonly List<Keys> _keysCopyList = new List<Keys>();

        /// <summary>
        /// Хранит последовательность нажатых клавиш для вставки
        /// </summary>
        readonly List<Keys> _keysPasteList = new List<Keys>();

        /// <summary>
        /// Хранит последовательность нажатых клавиш для отображения окна
        /// </summary>
        readonly List<Keys> _keysShowWindowList = new List<Keys>();

        /// <summary>
        /// Управляет глобальным перехватом нажатых клавиш. Если true - то перехват активен.
        /// </summary>
        bool _globalKeyDown = true;

        /// <summary>
        /// Перехватывает нажатие всех клавиш в системе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Нажатая клавиша</param>
        void InputController_KeyDown(object sender, KeyEventArgs e)
        {
            if(!_globalKeyDown)
            {
                return;
            }
            //ESC
            else if (e.KeyCode == Keys.Escape)
            {
                _keysCopyList.Clear();
                _keysPasteList.Clear();
                _keysShowWindowList.Clear();
                CopyPasteCancelled?.Invoke();
            }
            //LeftCtrl
            else if ((_keysCopyList.Count == 0 || _keysPasteList.Count == 0) && e.KeyCode == Keys.LControlKey)
            {
                _keysCopyList.Add(e.KeyCode);
                _keysPasteList.Add(e.KeyCode);
                _keysShowWindowList.Add(e.KeyCode);
            }
            //LeftCtrl + C
            else if (_keysCopyList.Count == 1 && _keysCopyList[0] == Keys.LControlKey && e.KeyCode == Keys.C)
            {
                _keysPasteList.Clear();
                _keysShowWindowList.Clear();
                _keysCopyList.Add(e.KeyCode);
                CopyIsActive?.Invoke();
            }
            //LeftCtrl + V
            else if(_keysPasteList.Count == 1 && _keysPasteList[0] == Keys.LControlKey && e.KeyCode == Keys.V)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                _keysCopyList.Clear();
                _keysShowWindowList.Clear();
                _keysPasteList.Add(e.KeyCode);
                PasteIsActive?.Invoke();
            }
            //LeftCtrl + ~
            else if(_keysShowWindowList.Count == 1 && _keysShowWindowList[0] == Keys.LControlKey && e.KeyCode == Keys.Oemtilde)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                _keysShowWindowList.Clear();
                _keysCopyList.Clear();
                _keysPasteList.Clear();
                ShowWindowKeyPress?.Invoke(this, EventArgs.Empty);
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
                PasteKeyPress?.Invoke(this, new InputHandlerEventArgs(_keysPasteList[2], string.Empty));
            }
            else
            {
                _keysShowWindowList.Clear();
                _keysCopyList.Clear();
                _keysPasteList.Clear();
            }
        }

        /// <summary>
        /// Изменилось значение буфера обмена Windows
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void ClipboardMonitor_ClipboardChanged(object sender, SharpClipboard.ClipboardChangedEventArgs e)
        {
            if (_keysPasteList.Count == 3 && _keysPasteList[0] == Keys.LControlKey && _keysPasteList[1] == Keys.V)
            {
                _globalKeyDown = false;
                InputSimulator inputSimulator = new InputSimulator();
                inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LCONTROL);
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_V);
                inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);
                _globalKeyDown = true;
                _keysPasteList.Clear();
                return;
            }

            if(e.ContentType == SharpClipboard.ContentTypes.Text && (_keysCopyList.Count == 1 || _keysCopyList.Count == 0))
            {
                CopyKeyPress?.Invoke(this, new InputHandlerEventArgs(Keys.Space, (string)e.Content));
            }

            await Task.Run(() =>
            {
                while (_keysCopyList.Count >= 2 && _keysCopyList[0] == Keys.LControlKey && _keysCopyList[1] == Keys.C)
                {

                    if (_keysCopyList.Count == 3)
                    {
                        if (e.ContentType == SharpClipboard.ContentTypes.Text)
                            CopyKeyPress?.Invoke(this, new InputHandlerEventArgs(_keysCopyList[2], (string)e.Content));
                        _keysCopyList.Clear();
                        return;
                    }
                }
            });
        }
    }
}
