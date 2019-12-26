using System;
using System.Windows.Input;
using MultiBuffer.WpfApp.Models.Interfaces;

namespace MultiBuffer.WpfApp.ViewModels.Services
{
    public class CommandFactory : ICommandFactory
    {
        public ICommand GetCommand(Action action)
        {
            return new Command(action);
        }
    }
}
