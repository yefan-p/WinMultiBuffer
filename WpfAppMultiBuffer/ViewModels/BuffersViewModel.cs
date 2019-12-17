using System.Collections.Generic;
using System.Collections.ObjectModel;
using WpfAppMultiBuffer.Models.Interfaces;
using WpfAppMultiBuffer.Utils;

namespace WpfAppMultiBuffer.ViewModels
{
    public class BuffersViewModel : BaseViewModel
    {
        /// <summary>
        /// Хранит коллекцию буферов
        /// </summary>
        public IList<IBufferItem> Buffers { get; }

        public BuffersViewModel(
                        INavigationManager navigationManager,
                        ICopyPasteController<IList<IBufferItem>> copyPasteController)
                        : base(navigationManager)
        {
            ICopyPasteController<IList<IBufferItem>> _copyPasteController = copyPasteController;
            _copyPasteController.Update += CopyPasteController_Update;

            Buffers = copyPasteController.Buffer;
        }

        private void CopyPasteController_Update(IBufferItem obj)
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
