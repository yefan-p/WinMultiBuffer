namespace MultiBuffer.WpfApp.Models.Interfaces
{
    public interface IClipboardController
    {
        /// <summary>
        /// Устанавливает текст в буфер обмена
        /// </summary>
        /// <param name="value">Текст, который необходимо передать</param>
        public void SetText(string value);

        /// <summary>
        /// Получает текст из буфера обмена
        /// </summary>
        /// <returns>Строка с текстом, полученная из буфера</returns>
        public string GetText();
    }
}
