using System;
using MultiBuffer.WpfApp.Models.Handlers;

namespace MultiBuffer.WpfApp.Models.Interfaces
{
    public interface IInputHandler
    {
        /// <summary>
        /// Указывает, что была нажата клавиша вставки
        /// </summary>
        event EventHandler<InputHandlerEventArgs> PasteKeyPress;

        /// <summary>
        /// Указывает, что была нажата клавиша копирования
        /// </summary>
        event EventHandler<InputHandlerEventArgs> CopyKeyPress;

        /// <summary>
        /// Указывает, что была нажата клавиша отображения окна
        /// </summary>
        event EventHandler ShowWindowKeyPress;

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
