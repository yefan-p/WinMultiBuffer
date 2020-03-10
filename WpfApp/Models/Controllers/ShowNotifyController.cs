using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiBuffer.WpfApp.Models.Interfaces;
using MultiBuffer.WpfApp.Utils;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MultiBuffer.WpfApp.Models.Controllers
{
    public class ShowNotifyController : IShowNotifyController, INotifyPropertyChanged
    {
        public ShowNotifyController(IInputHandler inputHandler,
                                    IList<IBufferItem> buffers,
                                    ITrayIconManager trayIconManager)
        {
            _buffers = buffers;
            _trayIconManager = trayIconManager;

            _trayIconManager.AddContextMenuItem("Buffers   LeftCtrl + ~", ShowBuffersHandler);
            _trayIconManager.AddContextMenuItem("Help", ShowHelpHandler);
            _trayIconManager.AddContextMenuItem("Close", CloseAppHandler);
            _trayIconManager.AddClickCommand(ShowBuffersHandler);

            inputHandler.CopyIsActive += InputHandler_CopyIsActive;
            inputHandler.PasteIsActive += InputHandler_PasteIsActive;
            inputHandler.CopyPasteCancelled += InputHandler_CopyPasteCancelled;
            inputHandler.ShowWindowKeyPress += InputHandler_ShowWindowKeyPress;
            inputHandler.CopyKeyPress += InputHandler_CopyKeyPress;
            inputHandler.PasteKeyPress += InputHandler_PasteKeyPress;
        }

        /// <summary>
        /// Загловок уведомления
        /// </summary>
        public string HeaderNotifyMessage 
        {
            get
            {
                return _headerNotifyMessage;
            } 
            private set
            {
                _headerNotifyMessage = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Текст уведомления
        /// </summary>
        public string BodyNotifyMessage 
        {
            get
            {
                return _bodyNotifyMessage;
            } 
            private set
            {
                _bodyNotifyMessage = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Свойство обновлено
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Был выполнен клик в контекстном меню "Buffers"
        /// </summary>
        public event Action ShowBuffersClick;

        /// <summary>
        /// Был выполнен клик в контекстном меню "Help"
        /// </summary>
        public event Action ShowHelpClick;

        /// <summary>
        /// Загловок уведомления
        /// </summary>
        string _headerNotifyMessage;

        /// <summary>
        /// Текст уведомления
        /// </summary>
        string _bodyNotifyMessage;

        /// <summary>
        /// Коллекция буферов
        /// </summary>
        readonly IList<IBufferItem> _buffers;

        /// <summary>
        /// Икона в трее
        /// </summary>
        readonly ITrayIconManager _trayIconManager;

        /// <summary>
        /// Обработчик события нажатия забинденой клавиши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void InputHandler_PasteKeyPress(object sender, Handlers.InputHandlerEventArgs e)
        {
            _trayIconManager.HideNotify();
        }

        /// <summary>
        /// Обработчик события бинда клавиши для копирования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void InputHandler_CopyKeyPress(object sender, Handlers.InputHandlerEventArgs e)
        {
            _trayIconManager.HideNotify();
        }

        /// <summary>
        /// Если нажата горячая клавиша для отображения главного окна, показываем его.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void InputHandler_ShowWindowKeyPress(object sender, EventArgs e)
        {
            ShowBuffersHandler();
        }

        /// <summary>
        /// Обработчик события после отмены копирования/вставки
        /// </summary>
        void InputHandler_CopyPasteCancelled()
        {
            _trayIconManager.HideNotify();
        }

        /// <summary>
        /// Обработчик события после активации всавтки
        /// </summary>
        void InputHandler_PasteIsActive()
        {
            HeaderNotifyMessage = "Press binded key";
            BodyNotifyMessage = "Press key, which you binded on time copy.";
            _trayIconManager.ShowNotify(60000);
        }

        /// <summary>
        /// Обработчик события после активации копирования
        /// </summary>
        void InputHandler_CopyIsActive()
        {
            HeaderNotifyMessage = "Bind any key for buffer";
            BodyNotifyMessage = "You can bind any key, expect &quot;Esc&quot; and &quot;Left Ctrl&quot;. After binded you might to use binded key for paste.";
            _trayIconManager.ShowNotify(60000);
        }

        /// <summary>
        /// Обработчик команды ShowBuffers
        /// </summary>
        void ShowBuffersHandler()
        {
            if (_buffers.Count != 0)
            {
                App.Current.MainWindow.Show();
                App.Current.MainWindow.Activate();
                ShowBuffersClick?.Invoke();
            }
            else
            {
                ShowHelpHandler();
            }
        }

        /// <summary>
        /// Обработчик команды ShowHelp
        /// </summary>
        void ShowHelpHandler()
        {
            App.Current.MainWindow.Show();
            App.Current.MainWindow.Activate();
            ShowHelpClick?.Invoke();
        }

        /// <summary>
        /// Обработчик команды CloseApp
        /// </summary>
        void CloseAppHandler()
        {
            App.Current.Shutdown();
        }

        /// <summary>
        /// Функция вызова события "Свойство обновленно"
        /// </summary>
        /// <param name="propertyName">Имя вызывающего метода или свойства. Заполняется автоматически</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
