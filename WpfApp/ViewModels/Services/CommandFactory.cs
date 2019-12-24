using System;
using System.Windows.Input;
using WpfAppMultiBuffer.Models.Interfaces;

namespace WpfAppMultiBuffer.ViewModels.Services
{
    public class CommandFactory : ICommandFactory
    {
        public ICommand GetCommand(Action action)
        {
            return new Command(action);
        }
    }
}
