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
        /// Копирование активно, ждет клавишу для бинда
        /// </summary>
        event Action<string, string> CopyIsActive;

        /// <summary>
        /// Вставка актина, ждет забинденную клавишу
        /// </summary>
        event Action<string, string> PasteIsActive;
    }
}
