using MultiBuffer.WpfApp.Models.Interfaces;
using MultiBuffer.WpfApp.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MultiBuffer.WpfApp.ViewModels
{
    public class WindowViewModel : BaseViewModel
    {
        public WindowViewModel(ICommandFactory commandFactory)
        {
            ShowBuffers = commandFactory.GetCommand(ShowBuffersHandler);
            ShowHelp = commandFactory.GetCommand(ShowHelpHandler);
            CloseApp = commandFactory.GetCommand(CloseAppHandler);
        }

        /// <summary>
        /// Показывает окно и view Buffers
        /// </summary>
        public ICommand ShowBuffers { get; }

        /// <summary>
        /// Показывает окно с подсказкой
        /// </summary>
        public ICommand ShowHelp { get; }

        /// <summary>
        /// Закрывает приложение
        /// </summary>
        public ICommand CloseApp { get; }

        private void ShowBuffersHandler()
        {
            Debug.WriteLine("Buffers view");
        }

        private void ShowHelpHandler()
        {
            Debug.WriteLine("Help view");
        }

        private void CloseAppHandler()
        {
            Debug.WriteLine("App closed!");
        }
    }
}
