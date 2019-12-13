using System.Collections.ObjectModel;
using WpfAppMultiBuffer.Interfaces;
using WpfAppMultiBuffer.Models;
using WpfAppMultiBuffer.Utils;

namespace WpfAppMultiBuffer.ViewModels
{
    public class BuffersViewModel : BaseViewModel
    {
        public ObservableCollection<BufferItem> Buffers { get; }

        public BuffersViewModel(
                        INavigationManager navigationManager,
                        ICopyPasteController<ObservableCollection<BufferItem>> copyPasteController)
                        : base(navigationManager)
        {
            _copyPasteController = copyPasteController;
            _copyPasteController.Update += CopyPasteController_Update;
            Buffers = copyPasteController.Buffer;
        }

        readonly ICopyPasteController<ObservableCollection<BufferItem>> _copyPasteController;

        private void CopyPasteController_Update(BufferItem obj)
        {
            if (Buffers.Count == 0)
            {
                NavigationManager.Navigate(NavigationKeys.HelpView);
            }
            else
            {
                NavigationManager.Navigate(NavigationKeys.BuffersView);
            }
        }
    }
}
