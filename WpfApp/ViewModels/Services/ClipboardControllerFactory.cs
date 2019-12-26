using MultiBuffer.WpfApp.Models.Interfaces;

namespace MultiBuffer.WpfApp.ViewModels.Services
{
    public class ClipboardControllerFactory : IClipboardControllerFactory
    {
        public IClipboardController GetClipboard()
        {
            return new ClipboardController();
        }
    }
}
