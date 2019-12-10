using System;
using System.Linq;
using NHotkey;
using NHotkey.Wpf;
using System.Windows.Input;
using Gma.System.MouseKeyHook;
using System.Windows.Forms;
using WpfAppMultiBuffer.Views;

namespace WpfAppMultiBuffer.Controllers
{
    /// <summary>
    /// Возвращает код нажатой клавиши, если до этого был нажат hotkey
    /// </summary>
    public class InputController
    {
        public InputController()
        {
            HotkeyManager.Current.AddOrReplace("ActivateMultiBufferWPF", Key.OemTilde, ModifierKeys.Control, ActivateBuffer);

            IKeyboardEvents keyboardEvents;
            keyboardEvents = Hook.GlobalEvents();
            keyboardEvents.KeyDown += KeyboardEvents_KeyDown;
        }
        /// <summary>
        /// Указывает, что была нажата клавиша вставки
        /// </summary>
        public event EventHandler<InputViewEventArgs> PasteKeyPress;
        /// <summary>
        /// Указывает, что была нажата клавиша копирования
        /// </summary>
        public event EventHandler<InputViewEventArgs> CopyKeyPress;
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
        /// Указываем, что hotkey нажат
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ActivateBuffer(object sender, HotkeyEventArgs e)
        {
            _isActive = true;
        }
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
                    CopyKeyPress?.Invoke(this, new InputViewEventArgs(e.KeyCode));
                }
                else if(KeysPaste.Contains(e.KeyCode))
                {
                    PasteKeyPress?.Invoke(this, new InputViewEventArgs(e.KeyCode));
                }
            }
        }
        /// <summary>
        /// Удаляет регистрацию горячих клавиш
        /// </summary>
        public void Dispose()
        {
            HotkeyManager.Current.Remove("ActivateMultiBufferWPF");
        }

        public static Keys GetKey(Keys key)
        {
            for (int i = 0; i < KeysPaste.Length; i++)
            {
                if (KeysPaste[i] == key)
                {
                    return KeysCopy[i];
                }
            }

            for (int i = 0; i < KeysCopy.Length; i++)
            {
                if (KeysCopy[i] == key)
                {
                    return KeysPaste[i];
                }
            }

            throw new Exception("Клавиша не найдена");
        }

        public static Keys GetPasteKey(Keys key)
        {
            for (int i = 0; i < KeysPaste.Length; i++)
            {
                if (KeysPaste[i] == key)
                {
                    return KeysPaste[i];
                }
            }

            throw new Exception("Клавиша не найдена");
        }

        public static Keys GetCopyKey(Keys key)
        {
            for (int i = 0; i < KeysCopy.Length; i++)
            {
                if (KeysCopy[i] == key)
                {
                    return KeysCopy[i];
                }
            }

            throw new Exception("Клавиша не найдена");
        }
    }
}
