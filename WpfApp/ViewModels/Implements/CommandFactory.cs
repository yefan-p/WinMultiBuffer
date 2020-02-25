using System;
using System.Windows.Input;
using MultiBuffer.WpfApp.Models.Interfaces;

namespace MultiBuffer.WpfApp.ViewModels.Implements
{
    public class CommandFactory : ICommandFactory
    {
        public ICommand GetCommand(Action action)
        {
            return new Command(action);
        }
    }
}
