using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MultiBuffer.WpfApp.Models.Interfaces;

namespace MultiBuffer.WpfApp.Models.Controllers
{
    /// <summary>
    /// Предоставляет текст для уведомлений
    /// </summary>
    public class TextMessageNotifyController : ITextMessageNotifyController
    {
        public TextMessageNotifyController(IInputHandler inputHandler)
        {
            inputHandler.CopyIsActive += InputHandler_CopyIsActive;
            inputHandler.PasteIsActive += InputHandler_PasteIsActive;
        }

        public event Action<string, string> CopyIsActive;

        public event Action<string, string> PasteIsActive;

        /// <summary>
        /// Обработчик события после активации всавтки
        /// </summary>
        void InputHandler_PasteIsActive()
        {
            string headerNotifyMessage = "Press binded key";
            string bodyNotifyMessage = "Press key, which you binded on time copy.";
            PasteIsActive?.Invoke(headerNotifyMessage, bodyNotifyMessage);
        }

        /// <summary>
        /// Обработчик события после активации копирования
        /// </summary>
        void InputHandler_CopyIsActive()
        {
            string headerNotifyMessage = "Bind any key for buffer";
            string bodyNotifyMessage = "You can bind any key, expect &quot;Esc&quot; and &quot;Left Ctrl&quot;. After binded you might to use binded key for paste.";
            CopyIsActive?.Invoke(headerNotifyMessage, bodyNotifyMessage);
        }
    }
}
