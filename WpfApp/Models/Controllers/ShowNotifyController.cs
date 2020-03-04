using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiBuffer.WpfApp.Models.Interfaces;
using Hardcodet.Wpf.TaskbarNotification;
using System.Windows.Controls;

namespace MultiBuffer.WpfApp.Models.Controllers
{
    public class ShowNotifyController : IShowNotifyController
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
        /// Обработчик события после активации копирования
        /// </summary>
        void InputHandler_CopyPasteCancelled()
        {
            _taskbarIcon.ShowBalloonTip("MultiBuffers",
                                        "Copy/Paste was cancelled.",
                                        _taskbarIcon.Icon);
        }

        /// <summary>
        /// Обработчик события после активации всавтки
        /// </summary>
        void InputHandler_PasteIsActive()
        {
            _taskbarIcon.ShowBalloonTip("MultiBuffers",
                                        "Press binded key.",
                                        _taskbarIcon.Icon);
        }

        /// <summary>
        /// Обработчик события после отмены копирования/вставки
        /// </summary>
        void InputHandler_CopyIsActive()
        {
            _taskbarIcon.ShowBalloonTip("MultiBuffers",
                            "Bind any key for buffer.",
                            _taskbarIcon.Icon);
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
