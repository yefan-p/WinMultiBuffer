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
                                   ICopyPasteController<IList<IBufferItem>> copyPasteController,
                                   IInputHandler inputHandler,
                                   IShowNotifyController showNotifyController)
        {
            ViewName = NavigationKeys.HelpView;

            ShowBuffers = commandFactory.GetCommand(ShowBuffersHandler);
            ShowHelp = commandFactory.GetCommand(ShowHelpHandler);
            CloseApp = commandFactory.GetCommand(CloseAppHandler);

            inputHandler.ShowWindowKeyPress += InputHandler_ShowWindowKeyPress;
            App.Current.MainWindow.Deactivated += MainWindow_Deactivated;

            _buffers = copyPasteController.Buffer;
            copyPasteController.Update += CopyPasteController_Update;
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
                if (_currentWindowState == WindowState.Minimized)
                {
                    App.Current.MainWindow.Hide();
                }
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Заголовок окна
        /// </summary>
        public string ViewName
        {
            get { return _viewName; }
            set
            {
                _viewName = value;
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
        /// Заголовок окна
        /// </summary>
        string _viewName;

        /// <summary>
        /// Хранит коллекцию буферов
        /// </summary>
        IList<IBufferItem> _buffers;

        /// <summary>
        /// Текущее состояние окна свернто/развернуто
        /// </summary>
        WindowState _currentWindowState;

        /// <summary>
        /// Обновляет заголовок окна после удаления последнего буфера
        /// </summary>
        /// <param name="obj"></param>
        void CopyPasteController_Update(IBufferItem obj)
        {
            if (_buffers.Count == 0)
            {
                ViewName = NavigationKeys.HelpView;
            }
        }

        /// <summary>
        /// Если нажата горячая клавиша для отображения главного окна, показываем его.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void InputHandler_ShowWindowKeyPress(object sender, EventArgs e)
        {
            ShowBuffersHandler();
            App.Current.MainWindow.Activate();
        }

        /// <summary>
        /// Если приложение потеряло фокус - скрываем его.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainWindow_Deactivated(object sender, EventArgs e)
        {
            CurrentWindowState = WindowState.Minimized;
        }

        /// <summary>
        /// Обработчик команды ShowBuffers
        /// </summary>
        private void ShowBuffersHandler()
        {
            if (_buffers.Count != 0)
            {
                NavigationManager.Navigate(NavigationKeys.BuffersView);
                ViewName = NavigationKeys.BuffersView;
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
            ViewName = NavigationKeys.HelpView;
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
