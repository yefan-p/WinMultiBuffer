using System;

namespace MultiBuffer.WpfApp.Models.Interfaces
{
    public interface IShowNotifyController
    {

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
