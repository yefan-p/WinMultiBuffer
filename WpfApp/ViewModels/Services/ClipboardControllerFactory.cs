using WpfAppMultiBuffer.Models.Interfaces;

namespace WpfAppMultiBuffer.ViewModels
{
    public class ClipboardControllerFactory : IClipboardControllerFactory
    {
        public IClipboardController GetClipboardController()
        {
            return new ClipboardController();
        }
    }
}
