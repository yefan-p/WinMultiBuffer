using MultiBuffer.WpfApp.Models.Interfaces;
using MultiBuffer.WpfApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Notifications.Wpf;

namespace MultiBuffer.WpfApp.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel(ICommandFactory commandFactory,
                                   ICopyPasteController<IList<IBufferItem>> copyPasteController,
                                   IInputHandler inputHandler)
        {
            ViewName = NavigationKeys.HelpView;

            ShowBuffers = commandFactory.GetCommand(ShowBuffersHandler);
            ShowHelp = commandFactory.GetCommand(ShowHelpHandler);
            CloseApp = commandFactory.GetCommand(CloseAppHandler);

            inputHandler.ShowWindowKeyPress += InputHandler_ShowWindowKeyPress;
            inputHandler.PasteKeyPress += InputHandler_PasteKeyPress;
            inputHandler.CopyKeyPress += InputHandler_CopyKeyPress;

            App.Current.MainWindow.Deactivated += MainWindow_Deactivated;

            Buffers = copyPasteController.Buffer;
            copyPasteController.Update += CopyPasteController_Update;
        }

        /// <summary>
        /// Обработчик события нажатия кнопки копирования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputHandler_CopyKeyPress(object sender, Models.Handlers.InputHandlerEventArgs e)
        {
            var notificationManager = new NotificationManager();
            notificationManager.Show(new NotificationContent
            {
                Title = "Sample notification",
                Message = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                Type = NotificationType.Information
            });
        }

        /// <summary>
        /// Обработчик события нажатия кнопки вставки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputHandler_PasteKeyPress(object sender, Models.Handlers.InputHandlerEventArgs e)
        {
            
        }

        /// <summary>
        /// Обновляет заголовок окна после удаления последнего буфера
        /// </summary>
        /// <param name="obj"></param>
        private void CopyPasteController_Update(IBufferItem obj)
        {
            if (Buffers.Count == 0)
            {
                ViewName = NavigationKeys.HelpView;
            }
        }

        /// <summary>
        /// Если нажата горячая клавиша для отображения главного окна, показываем его.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputHandler_ShowWindowKeyPress(object sender, EventArgs e)
        {
            ShowBuffersHandler();
            App.Current.MainWindow.Activate();
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

        /// <summary>
        /// Заголовок окна
        /// </summary>
        private string _viewName;

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
