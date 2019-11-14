using NHotkey;
using NHotkey.Wpf;
using System.Windows.Input;
using Gma.System.MouseKeyHook;
using WpfAppMultiBuffer.ViewModels;

namespace WpfAppMultiBuffer.Views
{
    class InputView
    {
        BufferViewModels _buffer;

        public InputView(BufferViewModels buffer)
        {
            _buffer = buffer;
            HotkeyManager.Current.AddOrReplace("ActivateMultiBufferWPF", Key.OemTilde, ModifierKeys.Control, ActivateBuffer);

            IKeyboardEvents keyboardEvents;
            keyboardEvents = Hook.GlobalEvents();
            keyboardEvents.KeyDown += KeyboardEvents_KeyDown;
        }

        void ActivateBuffer(object sender, HotkeyEventArgs e)
        {
            _buffer.IsActive = true;
        }

        private void KeyboardEvents_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (_buffer.IsActive)
            {
                e.SuppressKeyPress = true;
                _buffer.IsActive = false;
                _buffer.CopyPaste(e.KeyCode);
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
