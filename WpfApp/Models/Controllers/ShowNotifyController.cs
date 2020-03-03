using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiBuffer.WpfApp.Models.Interfaces;
using Notifications.Wpf;

namespace MultiBuffer.WpfApp.Models.Controllers
{
    public class ShowNotifyController : IShowNotifyController
    {
        public ShowNotifyController(IInputHandler inputHandler)
        {
            _notificationManager = new NotificationManager();

            inputHandler.CopyIsActive += InputHandler_CopyIsActive;
            inputHandler.PasteIsActive += InputHandler_PasteIsActive;
            inputHandler.CopyPasteCancelled += InputHandler_CopyPasteCancelled;
        }

        /// <summary>
        /// Событие возникает после нажатия клавиш LCtrl + C
        /// После него ожидается нажатие клавиши для вставки в буфер
        /// </summary>
        public event Action CopyIsActive;

        /// <summary>
        /// Событие возникает после нажатия клавиш LCtrl + V
        /// После него ожидается нажатие клавиши для вставки в буфер
        /// </summary>
        public event Action PasteIsActive;

        /// <summary>
        /// Отменяет ожидание клавиши после клавиш вставки/копирования
        /// </summary>
        public event Action CopyPasteCancelled;

        /// <summary>
        /// Отправляет уведомления
        /// </summary>
        NotificationManager _notificationManager;

        /// <summary>
        /// Обработчик события после активации копирования
        /// </summary>
        void InputHandler_CopyPasteCancelled()
        {
            _notificationManager.Show(new NotificationContent
            {
                Title = "MultiBuffers",
                Message = "Copy/Paste was cancelled.",
                Type = NotificationType.Error
            });
            CopyPasteCancelled?.Invoke();
        }

        /// <summary>
        /// Обработчик события после активации всавтки
        /// </summary>
        void InputHandler_PasteIsActive()
        {
            _notificationManager.Show(new NotificationContent
            {
                Title = "MultiBuffers",
                Message = "Press binded key.",
                Type = NotificationType.Information
            });
            PasteIsActive?.Invoke();
        }

        /// <summary>
        /// Обработчик события после отмены копирования/вставки
        /// </summary>
        void InputHandler_CopyIsActive()
        {
            _notificationManager.Show(new NotificationContent
            {
                Title = "MultiBuffers",
                Message = "Bind any key for buffer.",
                Type = NotificationType.Information
            });
            CopyIsActive?.Invoke();
        }
    }
}
