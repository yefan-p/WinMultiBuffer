using System;
using System.Windows.Input;

namespace WpfAppMultiBuffer.Models.Interfaces
{
    public interface ICommandFactory
    {
        ICommand GetCommand(Action action);
    }
}
