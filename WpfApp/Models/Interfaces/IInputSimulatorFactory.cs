using WindowsInput;

namespace WpfAppMultiBuffer.Models.Interfaces
{
    public interface IInputSimulatorFactory
    {
        IInputSimulator GetInputSimulator();
    }
}
