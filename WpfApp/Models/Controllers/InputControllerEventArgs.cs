using System;
using System.Windows.Forms;

namespace MultiBuffer.WpfApp.Models.Controllers
{
    public class InputControllerEventArgs : EventArgs
    {
        /// <summary>
        /// Передает сочетания клавиш, буфер которых нужно обработать
        /// </summary>
        /// <param name="copyKey">Клавиша копирования</param>
        /// <param name="pasteKey">Клавиша вставки</param>
        public InputControllerEventArgs(Keys copyKey, Keys pasteKey)
        {
            CopyKey = copyKey;
            PasteKey = pasteKey;
        }

        /// <summary>
        /// Клавиша копирования
        /// </summary>
        public readonly Keys CopyKey;

        /// <summary>
        /// Клавиша вставки
        /// </summary>
        public readonly Keys PasteKey;
    }
}
