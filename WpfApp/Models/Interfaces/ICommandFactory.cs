using System;
using System.Windows.Input;

namespace MultiBuffer.WpfApp.Models.Interfaces
{
    public interface ICommandFactory
    {
        ICommand GetCommand(Action action);
    }
}
