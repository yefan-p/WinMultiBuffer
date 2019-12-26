using WindowsInput;
using MultiBuffer.WpfApp.Models.Interfaces;

namespace MultiBuffer.WpfApp.ViewModels.Services
{
    public class InputSimulatorFactory : IInputSimulatorFactory
    {
        public IInputSimulator GetInputSimulator()
        {
            return new InputSimulator();
        }
    }
}
