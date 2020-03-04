using System;

namespace MultiBuffer.WpfApp.Models.Interfaces
{
    public interface IShowNotifyController
    {

        /// <summary>
        /// Был выполнен клик в контекстном меню "Buffers"
        /// </summary>
        public event Action ShowBuffersClick;

        /// <summary>
        /// Был выполнен клик в контекстном меню "Help"
        /// </summary>
        public event Action ShowHelpClick;
    }
}
