using System;

namespace MultiBuffer.WpfApp.Models.Interfaces
{
    public interface IShowNotifyController
    {

        /// <summary>
        /// Был выполнен клик в контекстном меню "Buffers"
        /// </summary>
        event Action ShowBuffersClick;

        /// <summary>
        /// Был выполнен клик в контекстном меню "Help"
        /// </summary>
        event Action ShowHelpClick;

        /// <summary>
        /// Загловок уведомления
        /// </summary>
        string HeaderNotifyMessage { get; }

        /// <summary>
        /// Текст уведомления
        /// </summary>
        string BodyNotifyMessage { get; }
    }
}
