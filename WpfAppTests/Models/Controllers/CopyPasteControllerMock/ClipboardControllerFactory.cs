using MultiBuffer.WpfApp.Models.Interfaces;

namespace MultiBuffer.WpfAppTests.Models.Controllers.CopyPasteControllerMock
{
    public class ClipboardControllerFactory : IClipboardControllerFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textClipboard">Текст по умолчанию для буфера обмена</param>
        public ClipboardControllerFactory(string textClipboard)
        {
            ClipboardController = new ClipboardController(textClipboard);
        }

        public ClipboardController ClipboardController { get; private set; }

        public IClipboardController GetClipboard()
        {
            return ClipboardController;
        }
    }
}
