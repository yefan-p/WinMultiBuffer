using MultiBuffer.WpfApp.Models.Interfaces;

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
        }
    }
}
