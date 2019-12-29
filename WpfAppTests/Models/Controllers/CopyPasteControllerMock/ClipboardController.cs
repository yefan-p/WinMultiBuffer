using MultiBuffer.WpfApp.Models.Interfaces;
using System;

namespace MultiBuffer.WpfAppTests.Models.Controllers.CopyPasteControllerMock
{
    public class ClipboardController : IClipboardController
    {
        /// <summary>
        /// Текст по умолчанию для буфера обмена
        /// </summary>
        /// <param name="defaultText"></param>
        public ClipboardController(string defaultText)
        {
            BufferText = defaultText;
        }

        public event Action<string> IsSetText;

        /// <summary>
        /// Текст в условном буфере
        /// </summary>
        public string BufferText { get; set; }

        public string GetText()
        {
            return BufferText;
        }

        public void SetText(string value)
        {
            BufferText = value;
            IsSetText?.Invoke(BufferText);
        }
    }
}