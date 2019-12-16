using WindowsInput;
using WpfAppMultiBuffer.Models.Interfaces;

namespace WpfAppMultiBuffer.ViewModels
{
    public class InputSimulatorFactory : IInputSimulatorFactory
    {
        public IInputSimulator GetInputSimulator()
        {
            return new InputSimulator();
        }
    }
}
