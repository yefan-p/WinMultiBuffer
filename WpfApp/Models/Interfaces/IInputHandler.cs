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
    }
}
