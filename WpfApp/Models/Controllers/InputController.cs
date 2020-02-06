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

        List<Keys> _keysList = new List<Keys>();

        private void InputController_KeyDown(object sender, KeyEventArgs e)
        {
            if (_keysList.Count == 2 && _keysList[0] == Keys.LControlKey && _keysList[1] == Keys.C)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                Debug.WriteLine(e.KeyCode);
                _keysList.Clear();
            }
            else if ((_keysList.Count == 0 && e.KeyCode == Keys.LControlKey) || (_keysList.Count == 1 && e.KeyCode == Keys.C))
                _keysList.Add(e.KeyCode);
            else
                _keysList.Clear();
        }

        /// <summary>
        /// Эмулирует нажатие клавиш
        /// </summary>
        private readonly IInputSimulator _inputSimulator;

        /// <summary>
        /// Нажата клавиша копирования
        /// </summary>
        void KeysCopyPressed()
        {
            Debug.WriteLine("OnCombination keys was pressed");
        }

        /// <summary>
        /// Нажата клавиша копирования
        /// </summary>
        void SequenceCopyPressed()
        {
            Debug.WriteLine("Sequence keys was pressed");
        }

        /// <summary>
        /// Нажата клавиша вставки
        /// </summary>
        void KeysPastePressed()
        {
            //Clipboard.Clear();
            _isCopyActive = false;
            _isPasteActive = true;

            IKeyboardEvents keyboardEvents;
            keyboardEvents = Hook.GlobalEvents();
            keyboardEvents.KeyDown += (obj, arg) =>
            {
                if(_isPasteActive)
                {
                    _isPasteActive = false;
                    arg.SuppressKeyPress = true;
                    arg.Handled = true;
                    PasteKeyPress?.Invoke(this, new InputControllerEventArgs(arg.KeyCode, string.Empty));
                }
            };
        }

        /// <summary>
        /// Изменилось значение буфера обмена Windows
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClipboardMonitor_ClipboardChanged(object sender, SharpClipboard.ClipboardChangedEventArgs e)
        {
            SharpClipboard clipboardMonitor = (SharpClipboard)sender;
            if (e.ContentType == SharpClipboard.ContentTypes.Text && _isCopyActive)
            {
                IKeyboardEvents keyboardEvents;
                keyboardEvents = Hook.GlobalEvents();
                keyboardEvents.KeyDown += (obj, arg) => 
                {
                    if (_isCopyActive)
                    {
                        arg.SuppressKeyPress = true;
                        arg.Handled = true;
                        _isCopyActive = false;
                        CopyKeyPress?.Invoke(this, new InputControllerEventArgs(arg.KeyCode, (string)e.Content));
                        arg.SuppressKeyPress = false;
                        arg.Handled = false;
                    }
                };
            }
            /*else if(e.ContentType == SharpClipboard.ContentTypes.Text && !_isPasteActive && !_isCopyActive)
            {
                Debug.WriteLine(e.Content);
                _isPasteActive = false;
                _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LCONTROL);
                _inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_V);
                _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);
            }*/
        }

        /// <summary>
        /// Указывает, что была нажата клавиша вставки
        /// </summary>
        public event EventHandler<InputControllerEventArgs> PasteKeyPress;

        /// <summary>
        /// Указывает, что была нажата клавиша копирования
        /// </summary>
        public event EventHandler<InputControllerEventArgs> CopyKeyPress;

        /// <summary>
        /// Флаг, который указывает, были ли нажаты клавиши активации буфера
        /// </summary>
        bool _isActive = false;

        /// <summary>
        /// Флаг, который указывает, были ли нажаты клавишы копирования
        /// </summary>
        private bool _isCopyActive = false;

        /// <summary>
        /// Флаг, который указывает, были ли нажаты клавиши вставки
        /// </summary>
        private bool _isPasteActive = false;

        /// <summary>
        /// Горячие клавиши для копирования
        /// </summary>
        public static readonly Keys[] KeysCopy =
        {
            Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9, Keys.D0, Keys.OemMinus, Keys.Oemplus,
            Keys.A, Keys.S, Keys.D, Keys.F, Keys.G, Keys.H, Keys.J, Keys.K, Keys.L
        };

        /// <summary>
        /// Горячие клавиши для вставки
        /// </summary>
        public static readonly Keys[] KeysPaste =
        {
            Keys.Q, Keys.W, Keys.E, Keys.R, Keys.T, Keys.Y, Keys.U, Keys.I, Keys.O, Keys.P, Keys.OemOpenBrackets, Keys.Oem6,
            Keys.Z, Keys.X, Keys.C, Keys.V, Keys.B, Keys.N, Keys.M, Keys.Oemcomma, Keys.OemPeriod
        };

        /// <summary>
        /// hotkey нажат, ожидаем нажатие следующей клавиши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyboardEvents_KeyDown(object sender, KeyEventArgs e)
        {
            if (_isActive)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                _isActive = false;

                if (KeysCopy.Contains(e.KeyCode))
                {
                    //CopyKeyPress?.Invoke(this, new InputControllerEventArgs(e.KeyCode, GetKey(e.KeyCode)));
                }
                else if(KeysPaste.Contains(e.KeyCode))
                {
                    //PasteKeyPress?.Invoke(this, new InputControllerEventArgs(GetKey(e.KeyCode), e.KeyCode));
                }
            }
        }

        /// <summary>
        /// Возвращает клавишу копирования если передана клавиша вставки, и возвращает клавишу вставки, если передана клавиша копирования
        /// </summary>
        /// <param name="key">Клавиша, для которой нужно получить соответствующую</param>
        /// <returns>Клавиша, соответствующая переданной</returns>
        public static Keys GetKey(Keys key)
        {
            if (KeysPaste.Length != KeysCopy.Length)
                throw new Exception("Keys copy and Keys paste must be same count");

            for (int i = 0; i < KeysPaste.Length; i++)
            {
                if (KeysPaste[i] == key)
                {
                    return KeysCopy[i];
                }
                else if(KeysCopy[i] == key)
                {
                    return KeysPaste[i];
                }
            }

            throw new Exception("Key not found");
        }
    }
}
