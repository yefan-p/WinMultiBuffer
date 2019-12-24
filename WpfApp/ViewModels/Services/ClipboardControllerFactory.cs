using WpfAppMultiBuffer.Models.Interfaces;

namespace WpfAppMultiBuffer.ViewModels.Services
{
    public class ClipboardControllerFactory : IClipboardControllerFactory
    {
        public IClipboardController GetClipboard()
        {
            return new ClipboardController();
        }
    }
}
