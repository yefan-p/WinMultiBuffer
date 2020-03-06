using System;
using Hardcodet.Wpf.TaskbarNotification;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using MultiBuffer.WpfApp.Models.Interfaces;

namespace MultiBuffer.WpfApp.Utils
{
    public class TrayIconManager : ITrayIconManager, IDisposable
    {
        public TrayIconManager(ICommandFactory commandFactory, UIElement notifyView)
        {
            _commandFactory = commandFactory;
            _notifyView = notifyView;
            _contextMenu = new ContextMenu();
            _taskbarIcon = new TaskbarIcon
            {
                ToolTipText = "MultiBuffer is runnig!",
                Icon = WpfApp.Properties.Resources.hook_icon,
                ContextMenu = _contextMenu
            };
        }

        public void Dispose()
        {
            _taskbarIcon.Dispose();
        }

        /// <summary>
        /// Добавляет пункт контексного меню для иконки в трее
        /// </summary>
        /// <param name="label">Метка, которая будет отображаться в меню</param>
        /// <param name="action">Действие, которое необходимо произвести при выборе пункта</param>
        public void AddContextMenuItem(string label, Action action)
        {
            var menuItem = new MenuItem
            {
                Command = _commandFactory.GetCommand(action),
                Header = label
            };
            _contextMenu.Items.Add(menuItem);
        }

        /// <summary>
        /// Показывает уведомление
        /// </summary>
        /// <param name="timeout">Время, которое будет отображаться уведомление</param>
        public void ShowNotify(int timeout)
        {
            _taskbarIcon.ShowCustomBalloon(_notifyView, PopupAnimation.Slide, timeout);
        }

        /// <summary>
        /// Скрывает уведомление
        /// </summary>
        public void HideNotify()
        {
            _taskbarIcon.CloseBalloon();
        }

        /// <summary>
        /// Устанавливает команду по клике на иконку в трее
        /// </summary>
        /// <param name="action">Команда</param>
        public void AddClickCommand(Action action)
        {
            _taskbarIcon.LeftClickCommand = _commandFactory.GetCommand(action);
            _taskbarIcon.DoubleClickCommand = _commandFactory.GetCommand(action);
        }

        /// <summary>
        /// Икона трея
        /// </summary>
        readonly TaskbarIcon _taskbarIcon;

        /// <summary>
        /// UserControl, который будет использоваться для отоброжения уведомления
        /// </summary>
        readonly UIElement _notifyView;

        /// <summary>
        /// Контекстное меню икноки трея
        /// </summary>
        readonly ContextMenu _contextMenu;

        readonly ICommandFactory _commandFactory;
    }
}
