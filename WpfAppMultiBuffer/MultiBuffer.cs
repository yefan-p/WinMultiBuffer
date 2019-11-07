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

namespace WpfAppMultiBuffer
{
    class MultiBuffer : IDisposable
    {
        public MultiBuffer()
        {
            HotkeyManager.Current.AddOrReplace("ActivateMultiBufferWPF", Key.OemTilde, ModifierKeys.Control, ActivateBuffer);

            IKeyboardEvents keyboardEvents;
            keyboardEvents = Hook.GlobalEvents();
            keyboardEvents.KeyDown += KeyboardEvents_KeyDown;
        }

        private void KeyboardEvents_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (_isActive)
            {
                e.SuppressKeyPress = true;
                _isActive = false;
                InputSimulator inputSimulator = new InputSimulator();
                Keys key = e.KeyCode;

                if (_keyCopy.Contains(key))
                {
                    inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LCONTROL);
                    inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_C);
                    inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);

                    Timer timer = new Timer();
                    timer.Tick += (timerInner, eventArgs) => Timer_Tick(timerInner, eventArgs, key);
                    timer.Interval = 250;
                    timer.Start();
                }
                else if (_keyPaste.Contains(key))
                {
                    if (_storage[key] != null && _storage[key] != "")
                        Clipboard.SetText(_storage[key]);

                    inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LCONTROL);
                    inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_V);
                    inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);
                }
            }
        }

        private void Timer_Tick(object timerInner, EventArgs eventArgs, Keys key)
        {
            Timer timer = (Timer)timerInner;
            timer.Stop();
            _storage[key] = Clipboard.GetText(TextDataFormat.UnicodeText);

            MultiBufferEventArgs bufferArgs = new MultiBufferEventArgs(_storage);
            BufferUpdate?.Invoke(this, bufferArgs);
        }

        bool _isActive = false;

        void ActivateBuffer(object sender, HotkeyEventArgs e)
        {
            _isActive = true;
        }

        public void Dispose()
        {
            HotkeyManager.Current.Remove("ActivateMultiBufferWPF");
        }
    }
}
