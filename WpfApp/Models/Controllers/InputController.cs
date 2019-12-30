using System;
using System.Linq;
using Gma.System.MouseKeyHook;
using System.Windows.Forms;
using MultiBuffer.WpfApp.Models.Interfaces;
using System.Diagnostics;
using System.Collections.Generic;
using WK.Libraries.SharpClipboardNS;

namespace MultiBuffer.WpfApp.Models.Controllers
{
    public class InputController : IInputController
    {
        public InputController()
        {
            var clipboardMonitor = new SharpClipboard();
            clipboardMonitor.ClipboardChanged += ClipboardMonitor_ClipboardChanged;

            IKeyboardEvents keyboardEvents;
            keyboardEvents = Hook.GlobalEvents();
            keyboardEvents.KeyDown += KeyboardEvents_KeyDown;

            Hook.GlobalEvents().OnCombination(new Dictionary<Combination, Action>
            {
                {
                    Combination.FromString("Control+C"), 
                    KeysCopyPressed
                },
                {
                    Combination.FromString("Control+V"),
                    KeysPastePressed
                }
            });
        }

        void KeysCopyPressed()
        {
            Debug.WriteLine("HookKeysCopy");
        }

        void KeysPastePressed()
        {
            Debug.WriteLine("HookKeysPaste");
        }

        private void ClipboardMonitor_ClipboardChanged(object sender, SharpClipboard.ClipboardChangedEventArgs e)
        {
            SharpClipboard clipboardMonitor = (SharpClipboard)sender;
            if (e.ContentType == SharpClipboard.ContentTypes.Text)
            {
                Debug.WriteLine("SharpClipboard");
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

        /// <summary>
        /// Флаг, который указывает, были ли нажаты клавиши активации буфера
        /// </summary>
        bool _isActive = false;

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
        private void KeyboardEvents_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (_isActive)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                _isActive = false;

                if (KeysCopy.Contains(e.KeyCode))
                {
                    CopyKeyPress?.Invoke(this, new InputControllerEventArgs(e.KeyCode, GetKey(e.KeyCode)));
                }
                else if(KeysPaste.Contains(e.KeyCode))
                {
                    PasteKeyPress?.Invoke(this, new InputControllerEventArgs(GetKey(e.KeyCode), e.KeyCode));
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
