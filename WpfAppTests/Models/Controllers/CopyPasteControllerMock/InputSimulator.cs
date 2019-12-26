using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;

namespace MultiBuffer.WpfAppTests.Models.Controllers.CopyPasteControllerMock
{
    public class InputSimulator : IInputSimulator
    {
        public IKeyboardSimulator Keyboard => new KeyboardSimulator();

        public IMouseSimulator Mouse => throw new NotImplementedException();

        public IInputDeviceStateAdaptor InputDeviceState => throw new NotImplementedException();
    }
}
