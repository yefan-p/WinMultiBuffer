using System;
using WindowsInput;
using MultiBuffer.WpfApp.Models.Interfaces;

namespace MultiBuffer.WpfAppTests.Models.Controllers.CopyPasteControllerTestsMock
{
    public class InputSimulatorFactory : IInputSimulatorFactory
    {
        public IInputSimulator GetInputSimulator()
        {
            throw new NotImplementedException();
        }
    }
}
