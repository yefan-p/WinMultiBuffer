using System;
using System.Windows.Input;

namespace MultiBuffer.WpfApp.ViewModels.Services
{
    public class Command : ICommand
    {
        public Command(Action action)
        {
            this.action = action;
        }

        public event EventHandler CanExecuteChanged;

        private readonly Action action;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action?.Invoke();
        }
    }
}
