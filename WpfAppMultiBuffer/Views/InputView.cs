using System;
using NHotkey;
using NHotkey.Wpf;
using System.Windows.Input;
using Gma.System.MouseKeyHook;

namespace WpfAppMultiBuffer.Views
{
    /// <summary>
    /// Возвращает код нажатой клавиши, если до этого был нажат hotkey
    /// </summary>
    class InputView
    {
        public event EventHandler<InputViewEventArgs> KeyDown;
        bool _isActive = false;

        public InputView()
        {
            HotkeyManager.Current.AddOrReplace("ActivateMultiBufferWPF", Key.OemTilde, ModifierKeys.Control, ActivateBuffer);

            IKeyboardEvents keyboardEvents;
            keyboardEvents = Hook.GlobalEvents();
            keyboardEvents.KeyDown += KeyboardEvents_KeyDown;
        }
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
                _isActive = false;
                KeyDown?.Invoke(this, new InputViewEventArgs(e.KeyCode));
            }
        }
        /// <summary>
        /// Удаляет регистрацию горячих клавиш
        /// </summary>
        public void Dispose()
        {
            HotkeyManager.Current.Remove("ActivateMultiBufferWPF");
        }
    }
}
