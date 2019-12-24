using WpfAppMultiBuffer.Models.Interfaces;
using TextCopy;

namespace WpfAppMultiBuffer.Models.Controllers
{
    public class ClipboardController : IClipboardController
    {
        /// <summary>
        /// Устанавливает текст в буфер обмена
        /// </summary>
        /// <param name="value">Текст, который необходимо передать</param>
        public string GetText()
        {
            return Clipboard.GetText();
        }

        /// <summary>
        /// Получает текст из буфера обмена
        /// </summary>
        /// <returns>Строка с текстом, полученная из буфера</returns>
        public void SetText(string value)
        {
            Clipboard.SetText(value);
        }
    }
}
