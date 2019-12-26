using WindowsInput;
using MultiBuffer.WpfApp.Models.Interfaces;

namespace MultiBuffer.WpfAppTests.Models.Controllers.CopyPasteControllerMock
{
    public class InputSimulatorFactory : IInputSimulatorFactory
    {
        public IInputSimulator GetInputSimulator()
        {
            return new InputSimulator();
        }
    }
}
