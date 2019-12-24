using WindowsInput;
using WpfAppMultiBuffer.Models.Interfaces;

namespace WpfAppMultiBuffer.ViewModels.Services
{
    public class InputSimulatorFactory : IInputSimulatorFactory
    {
        public IInputSimulator GetInputSimulator()
        {
            return new InputSimulator();
        }
    }
}
