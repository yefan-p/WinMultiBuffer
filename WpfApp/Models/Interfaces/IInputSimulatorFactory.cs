using WindowsInput;

namespace MultiBuffer.WpfApp.Models.Interfaces
{
    public interface IInputSimulatorFactory
    {
        IInputSimulator GetInputSimulator();
    }
}
