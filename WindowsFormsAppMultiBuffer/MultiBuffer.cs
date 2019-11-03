using System;
using System.Linq;
using System.Windows.Forms;
using NHotkey;
using NHotkey.WindowsForms;
using WindowsInput;
using Gma.System.MouseKeyHook;
using WindowsInput.Native;

namespace WindowsFormsAppMultiBuffer
{
    class MultiBuffer : IDisposable
    {
        #region Outer member
        public MultiBuffer()
        {
            for (int i = 0; i < _keyCopy.Length; i++)
            {
                _storage.Add(_keyCopy[i], _keyPaste[i], "");
            }

            HotkeyManager.Current.AddOrReplace("ActivateMultiBuffer", Keys.Oemtilde | Keys.Control, ActivateBuffer);

            IKeyboardEvents keyboardEvents;
            keyboardEvents = Hook.GlobalEvents();
            keyboardEvents.KeyDown += KeyboardEvents_KeyDown;
        }

        public event EventHandler<MultiBufferEventArgs> BufferUpdate;

        public TwiceKeyDictionary<Keys, string> Storage 
        { 
            get { return _storage; } 
        }

        public void Dispose()
        {
            HotkeyManager.Current.Remove("ActivateMultiBuffer");
        }
        #endregion

        #region Inner member
        readonly TwiceKeyDictionary<Keys, string> _storage = new TwiceKeyDictionary<Keys, string>();

        readonly Keys[] _keyCopy =
            {
                Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9, Keys.D0, Keys.OemMinus, Keys.Oemplus,
                Keys.A, Keys.S, Keys.D, Keys.F, Keys.G, Keys.H, Keys.J, Keys.K, Keys.L
            };

        readonly Keys[] _keyPaste =
            {
                Keys.Q, Keys.W, Keys.E, Keys.R, Keys.T, Keys.Y, Keys.U, Keys.I, Keys.O, Keys.P, Keys.OemOpenBrackets, Keys.Oem6,
                Keys.Z, Keys.X, Keys.C, Keys.V, Keys.B, Keys.N, Keys.M, Keys.Oemcomma, Keys.OemPeriod
            };

        bool IsActive = false;

        private void ActivateBuffer(object sender, HotkeyEventArgs e)
        {
            IsActive = true;
        }

        private void KeyboardEvents_KeyDown(object sender, KeyEventArgs e)
        {
            if (IsActive)
            {
                e.SuppressKeyPress = true;
                IsActive = false;
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
        #endregion
    }
}
