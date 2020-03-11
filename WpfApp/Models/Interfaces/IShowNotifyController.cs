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
    }
}
