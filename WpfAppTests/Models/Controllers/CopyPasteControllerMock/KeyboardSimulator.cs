using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;

namespace MultiBuffer.WpfAppTests.Models.Controllers.CopyPasteControllerMock
{
    public class KeyboardSimulator : IKeyboardSimulator
    {
        public IMouseSimulator Mouse => throw new NotImplementedException();

        public IKeyboardSimulator KeyDown(VirtualKeyCode keyCode)
        {
            return new KeyboardSimulator();
        }

        public IKeyboardSimulator KeyPress(VirtualKeyCode keyCode)
        {
            return new KeyboardSimulator();
        }

        public IKeyboardSimulator KeyPress(params VirtualKeyCode[] keyCodes)
        {
            throw new NotImplementedException();
        }

        public IKeyboardSimulator KeyUp(VirtualKeyCode keyCode)
        {
            return new KeyboardSimulator();
        }

        public IKeyboardSimulator ModifiedKeyStroke(IEnumerable<VirtualKeyCode> modifierKeyCodes, IEnumerable<VirtualKeyCode> keyCodes)
        {
            throw new NotImplementedException();
        }

        public IKeyboardSimulator ModifiedKeyStroke(IEnumerable<VirtualKeyCode> modifierKeyCodes, VirtualKeyCode keyCode)
        {
            throw new NotImplementedException();
        }

        public IKeyboardSimulator ModifiedKeyStroke(VirtualKeyCode modifierKey, IEnumerable<VirtualKeyCode> keyCodes)
        {
            throw new NotImplementedException();
        }

        public IKeyboardSimulator ModifiedKeyStroke(VirtualKeyCode modifierKeyCode, VirtualKeyCode keyCode)
        {
            throw new NotImplementedException();
        }

        public IKeyboardSimulator Sleep(int millsecondsTimeout)
        {
            throw new NotImplementedException();
        }

        public IKeyboardSimulator Sleep(TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        public IKeyboardSimulator TextEntry(string text)
        {
            throw new NotImplementedException();
        }

        public IKeyboardSimulator TextEntry(char character)
        {
            throw new NotImplementedException();
        }
    }
}
