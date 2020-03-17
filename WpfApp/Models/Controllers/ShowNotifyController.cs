using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiBuffer.WpfApp.Models.Interfaces;
using MultiBuffer.WpfApp.Utils;

namespace MultiBuffer.WpfApp.Models.Controllers
{
    public class ShowNotifyController : IShowNotifyController
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
        /// Был выполнен клик в контекстном меню "Buffers"
        /// </summary>
        public event Action ShowBuffersClick;

        /// <summary>
        /// Был выполнен клик в контекстном меню "Help"
        /// </summary>
        public event Action ShowHelpClick;

        /// <summary>
        /// Копирование активно, ждет клавишу для бинда
        /// </summary>
        public event Action<string, string> CopyIsActive;

        /// <summary>
        /// Вставка актина, ждет забинденную клавишу
        /// </summary>
        public event Action<string, string> PasteIsActive;

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
            string headerNotifyMessage = "Press binded key";
            string bodyNotifyMessage = "Press key, which you binded on time copy.";
            PasteIsActive?.Invoke(headerNotifyMessage, bodyNotifyMessage);
            _trayIconManager.ShowNotify(60000);
        }

        /// <summary>
        /// Обработчик события после активации копирования
        /// </summary>
        void InputHandler_CopyIsActive()
        {
            string headerNotifyMessage = "Bind any key for buffer";
            string bodyNotifyMessage = "You can bind any key, expect \"Esc\" and \"Left Ctrl\" After binded you might to use binded key for paste.";
            CopyIsActive?.Invoke(headerNotifyMessage, bodyNotifyMessage);
            _trayIconManager.ShowNotify(60000);
        }

        #region Команды контекстного меню
        /// <summary>
        /// Обработчик команды ShowBuffers, кнопка показать буферы в контекстном меню
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
        /// Обработчик команды ShowHelp, кнопка показать справку в контекстном меню
        /// </summary>
        void ShowHelpHandler()
        {
            App.Current.MainWindow.Show();
            App.Current.MainWindow.Activate();
            ShowHelpClick?.Invoke();
        }

        /// <summary>
        /// Обработчик команды CloseApp, кнопка закрыть в контекстном меню
        /// </summary>
        void CloseAppHandler()
        {
            App.Current.Shutdown();
        }
        #endregion
    }
}
