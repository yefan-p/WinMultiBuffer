using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiBuffer.WpfApp.Models.Interfaces;
using Hardcodet.Wpf.TaskbarNotification;
using System.Windows.Controls;
using MultiBuffer.WpfApp.Views.Notifications;
using System.Windows.Controls.Primitives;

namespace MultiBuffer.WpfApp.Models.Controllers
{
    public class ShowNotifyController : IShowNotifyController, IDisposable
    {
        public ShowNotifyController(IInputHandler inputHandler,
                                    ICommandFactory commandFactory,
                                    IList<IBufferItem> buffers)
        {
            _buffers = buffers;

            var buffersMenuItem = new MenuItem
            {
                Command = commandFactory.GetCommand(ShowBuffersHandler),
                Header = "Buffers   LeftCtrl + ~"
            };
            var helpMenuItem = new MenuItem
            {
                Command = commandFactory.GetCommand(ShowBuffersHandler),
                Header = "Help"
            };
            var closeMenuItem = new MenuItem
            {
                Command = commandFactory.GetCommand(CloseAppHandler),
                Header = "Close"
            };

            var contextMenu = new ContextMenu();
            contextMenu.Items.Add(buffersMenuItem);
            contextMenu.Items.Add(helpMenuItem);
            contextMenu.Items.Add(closeMenuItem);

            _taskbarIcon = new TaskbarIcon
            {
                ToolTipText = "MultiBuffer is runnig!",
                Icon = WpfApp.Properties.Resources.hook_icon,
                LeftClickCommand = commandFactory.GetCommand(ShowBuffersHandler),
                DoubleClickCommand = commandFactory.GetCommand(ShowBuffersHandler),
                ContextMenu = contextMenu
            };

            inputHandler.CopyIsActive += InputHandler_CopyIsActive;
            inputHandler.PasteIsActive += InputHandler_PasteIsActive;
            inputHandler.CopyPasteCancelled += InputHandler_CopyPasteCancelled;
            inputHandler.ShowWindowKeyPress += InputHandler_ShowWindowKeyPress;
            inputHandler.CopyKeyPress += InputHandler_CopyKeyPress;
            inputHandler.PasteKeyPress += InputHandler_PasteKeyPress;
        }

        public void Dispose()
        {
            _taskbarIcon.Dispose();
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
        /// Коллекция буферов
        /// </summary>
        readonly IList<IBufferItem> _buffers;

        /// <summary>
        /// Икона в трее
        /// </summary>
        readonly TaskbarIcon _taskbarIcon;

        /// <summary>
        /// Обработчик события нажатия забинденой клавиши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void InputHandler_PasteKeyPress(object sender, Handlers.InputHandlerEventArgs e)
        {
            _taskbarIcon.CloseBalloon();
        }

        /// <summary>
        /// Обработчик события бинда клавиши для копирования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void InputHandler_CopyKeyPress(object sender, Handlers.InputHandlerEventArgs e)
        {
            _taskbarIcon.CloseBalloon();
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
        /// Обработчик события после активации копирования
        /// </summary>
        void InputHandler_CopyPasteCancelled()
        {
            _taskbarIcon.CloseBalloon();
        }

        /// <summary>
        /// Обработчик события после активации всавтки
        /// </summary>
        void InputHandler_PasteIsActive()
        {
            var pasteNotifyView = new PasteNotifyView();
            _taskbarIcon.ShowCustomBalloon(pasteNotifyView, PopupAnimation.Slide, 60000);
        }

        /// <summary>
        /// Обработчик события после отмены копирования/вставки
        /// </summary>
        void InputHandler_CopyIsActive()
        {
            var copyNotifyView = new CopyNotifyView();
            _taskbarIcon.ShowCustomBalloon(copyNotifyView, PopupAnimation.Slide, 60000);
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
    }
}
