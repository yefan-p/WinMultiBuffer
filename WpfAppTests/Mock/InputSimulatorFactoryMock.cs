using System;
using WindowsInput;
using WpfAppMultiBuffer.Models.Interfaces;

namespace WpfAppMultiBufferTests.Mock
{
    public class InputSimulatorFactoryMock : IInputSimulatorFactory
    {
        public IInputSimulator GetInputSimulator()
        {
            throw new NotImplementedException();
        }
    }
}
