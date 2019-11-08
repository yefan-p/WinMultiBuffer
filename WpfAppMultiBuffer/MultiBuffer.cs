using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHotkey;
using NHotkey.Wpf;
using System.Windows.Input;
using System.Windows;
using Gma.System.MouseKeyHook;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;
using TextCopy;

namespace WpfAppMultiBuffer
{
    class MultiBuffer : IDisposable
    {
        public MultiBuffer()
        {
            HotkeyManager.Current.AddOrReplace("ActivateMultiBufferWPF", Key.OemTilde, ModifierKeys.Control, ActivateBuffer);

            _storage.AddRange(MultiBufferLiterals.KeysCopy, MultiBufferLiterals.KeysPaste, "");

            IKeyboardEvents keyboardEvents;
            keyboardEvents = Hook.GlobalEvents();
            keyboardEvents.KeyDown += KeyboardEvents_KeyDown;
        }

        /// <summary>
        /// Освобождаем занятые HotKeys
        /// </summary>
        public void Dispose()
        {
            HotkeyManager.Current.Remove("ActivateMultiBufferWPF");
        }

        /// <summary>
        /// В буфер добавлен новый текст
        /// </summary>
        public event EventHandler<EventArgs> BufferUpdate;
        bool _isActive = false;
        readonly TwiceKeyDictionary<Keys, string> _storage = new TwiceKeyDictionary<Keys, string>();

        private void KeyboardEvents_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (_isActive)
            {
                e.SuppressKeyPress = true;
                _isActive = false;
                InputSimulator inputSimulator = new InputSimulator();
                Keys key = e.KeyCode;

                if (MultiBufferLiterals.KeysCopy.Contains(key))
                {
                    string contentsClipboard = TextCopy.Clipboard.GetText() ?? "";
                    inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LCONTROL);
                    inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_C);
                    inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);

                    Timer timer = new Timer();
                    timer.Tick += (timerInner, eventArgs) =>
                        {
                            timer.Stop();
                            _storage[key] = TextCopy.Clipboard.GetText();
                            BufferUpdate?.Invoke(this, EventArgs.Empty);
                            TextCopy.Clipboard.SetText(contentsClipboard);
                        };
                    timer.Interval = MultiBufferLiterals.IntervalCopy;
                    timer.Start();
                }
                else if (MultiBufferLiterals.KeysPaste.Contains(key) && _storage[key] != null && _storage[key] != "")
                {
                    string contentsClipboard = TextCopy.Clipboard.GetText() ?? "";
                    TextCopy.Clipboard.SetText(_storage[key]);
                    inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LCONTROL);
                    inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_V);
                    inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);

                    Timer timer = new Timer();
                    timer.Tick += (timerInner, eventArgs) =>
                        {
                            timer.Stop();
                            TextCopy.Clipboard.SetText(contentsClipboard);
                        };
                    timer.Interval = MultiBufferLiterals.IntervalCopy;
                    timer.Start();
                }
            }
        }

        void ActivateBuffer(object sender, HotkeyEventArgs e)
        {
            _isActive = true;
        }
    }
}
