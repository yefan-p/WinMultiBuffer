using System.Collections.ObjectModel;
using WpfAppMultiBuffer.Interfaces;
using WpfAppMultiBuffer.Models;
using WpfAppMultiBuffer.Utils;

namespace WpfAppMultiBuffer.ViewModels
{
    public class BuffersViewModel : BaseViewModel
    {

        public BuffersViewModel(
                        INavigationManager navigationManager,
                        ICopyPasteController copyPasteController)
                        : base(navigationManager)
        {
            Buffers = new ObservableCollection<BufferItem>();

            _copyPasteController = copyPasteController;

            copyPasteController.Update += CopyPasteController_Update;
        }

        readonly ICopyPasteController _copyPasteController;

        private void CopyPasteController_Update(BufferItem obj)
        {
            int index = Buffers.IndexOf(obj);

            if(index == -1)
            {
                Buffers.Add(obj);
            }
            else
            {
                Buffers[index].Value = obj.Value;
            }

            if (Buffers.Count == 0)
            {
                NavigationManager.Navigate(NavigationKeys.HelpView);
            }
            else
            {
                NavigationManager.Navigate(NavigationKeys.BuffersView);
            }
        }

        /// <summary>
        /// Хранит информацию о существующих буферах
        /// </summary>
        public ObservableCollection<BufferItem> Buffers { get; private set; }
    }
}
