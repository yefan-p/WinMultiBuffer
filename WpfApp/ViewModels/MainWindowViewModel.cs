using MultiBuffer.WpfApp.Models.Interfaces;
using MultiBuffer.WpfApp.Utils;
using System;
using System.Collections.Generic;
using System.Windows;

namespace MultiBuffer.WpfApp.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel(ICopyPasteController<IList<IBufferItem>> copyPasteController,
                                   IShowNotifyController showNotifyController)
        {
            ViewName = NavigationKeys.HelpView;
            App.Current.MainWindow.Deactivated += MainWindow_Deactivated;

            _buffers = copyPasteController.Buffer;
            copyPasteController.Update += CopyPasteController_Update;

            showNotifyController.ShowBuffersClick += ShowNotifyController_ShowBuffersClick;
            showNotifyController.ShowHelpClick += ShowNotifyController_ShowHelpClick;
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

        /// <summary>
        /// Заголовок окна
        /// </summary>
        string _viewName;

        /// <summary>
        /// Хранит коллекцию буферов
        /// </summary>
        readonly IList<IBufferItem> _buffers;

        /// <summary>
        /// Текущее состояние окна свернто/развернуто
        /// </summary>
        WindowState _currentWindowState;

        /// <summary>
        /// Обрабобтчик события в контекстом меню выбран пункт Help
        /// </summary>
        void ShowNotifyController_ShowHelpClick()
        {
            NavigationManager.Navigate(NavigationKeys.HelpView);
            CurrentWindowState = WindowState.Normal;
            ViewName = NavigationKeys.HelpView;
        }

        /// <summary>
        /// Обработчик события в контекстом меню выбран пункт Buffers
        /// </summary>
        void ShowNotifyController_ShowBuffersClick()
        {
            NavigationManager.Navigate(NavigationKeys.BuffersView);
            CurrentWindowState = WindowState.Normal;
            ViewName = NavigationKeys.BuffersView;
        }

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
        /// Если приложение потеряло фокус - скрываем его.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainWindow_Deactivated(object sender, EventArgs e)
        {
            CurrentWindowState = WindowState.Minimized;
        }
    }
}
