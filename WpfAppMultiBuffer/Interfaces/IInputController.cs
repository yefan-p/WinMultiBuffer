using System;
using WpfAppMultiBuffer.Controllers;

namespace WpfAppMultiBuffer.Interfaces
{
    public interface IInputController
    {
        /// <summary>
        /// Указывает, что была нажата клавиша вставки
        /// </summary>
        event EventHandler<InputControllerEventArgs> PasteKeyPress;

        /// <summary>
        /// Указывает, что была нажата клавиша копирования
        /// </summary>
        event EventHandler<InputControllerEventArgs> CopyKeyPress;
    }
}
