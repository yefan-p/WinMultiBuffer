using MultiBuffer.WpfApp.Models.Interfaces;
using MultiBuffer.WpfApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace MultiBuffer.WpfApp.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel(ICommandFactory commandFactory, 
                                   IList<IBufferItem> buffers)
        {
            ShowBuffers = commandFactory.GetCommand(ShowBuffersHandler);
            ShowHelp = commandFactory.GetCommand(ShowHelpHandler);
            CloseApp = commandFactory.GetCommand(CloseAppHandler);
            App.Current.MainWindow.Deactivated += MainWindow_Deactivated;

            Buffers = buffers;
        }

        /// <summary>
        /// Если приложение потеряло фокус - скрываем его.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Deactivated(object sender, EventArgs e)
        {
            CurrentWindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Текущее состояние окна - свернто/развернуто
        /// </summary>
        public WindowState CurrentWindowState
        {
            get { return _currentWindowState; }
            set
            {
                _currentWindowState = value;
                if(_currentWindowState == WindowState.Minimized)
                {
                    App.Current.MainWindow.Hide();
                }
                OnPropertyChanged();
            }
        }

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

        /// <summary>
        /// Хранит коллекцию буферов
        /// </summary>
        private IList<IBufferItem> Buffers { get; }

        /// <summary>
        /// Текущее состояние окна свернто/развернуто
        /// </summary>
        private WindowState _currentWindowState;

        /// <summary>
        /// Обработчик команды ShowBuffers
        /// </summary>
        private void ShowBuffersHandler()
        {
            if (Buffers.Count != 0)
            {
                NavigationManager.Navigate(NavigationKeys.BuffersView);
                App.Current.MainWindow.Show();
                CurrentWindowState = WindowState.Normal;
            }
            else
            {
                ShowHelpHandler();
            }
        }

        /// <summary>
        /// Обработчик команды ShowHelp
        /// </summary>
        private void ShowHelpHandler()
        {
            NavigationManager.Navigate(NavigationKeys.HelpView);
            App.Current.MainWindow.Show();
            CurrentWindowState = WindowState.Normal;
        }

        /// <summary>
        /// Обработчик команды CloseApp
        /// </summary>
        private void CloseAppHandler()
        {
            App.Current.Shutdown();
        }
    }
}
