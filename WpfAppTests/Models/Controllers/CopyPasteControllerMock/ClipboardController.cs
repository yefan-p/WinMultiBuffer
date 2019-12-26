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
            _textBuffer = defaultText;
        }

        public event Action<string> TextWasSet;

        /// <summary>
        /// Хранит значение буфера обмена
        /// </summary>
        private string _textBuffer;

        public string GetText()
        {
            return _textBuffer;
        }

        public void SetText(string value)
        {
            _textBuffer = value;
            TextWasSet?.Invoke(value);
        }
    }
}
