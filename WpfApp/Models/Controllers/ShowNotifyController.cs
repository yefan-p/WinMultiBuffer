using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiBuffer.WpfApp.Models.Interfaces;
using Notifications.Wpf;

namespace MultiBuffer.WpfApp.Models.Controllers
{
    public class ShowNotifyController
    {
        public ShowNotifyController(IInputHandler inputHandler)
        {
            _notificationManager = new NotificationManager();
        }

        /// <summary>
        /// Отправляет уведомления
        /// </summary>
        NotificationManager _notificationManager;

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
    }
}
