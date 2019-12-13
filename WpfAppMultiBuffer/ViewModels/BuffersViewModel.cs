using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using WindowsInput;
using WindowsInput.Native;
using WpfAppMultiBuffer.Controllers;
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
            _navigationManager = navigationManager;

            copyPasteController.Update += CopyPasteController_Update;
        }

        readonly ICopyPasteController _copyPasteController;
        readonly INavigationManager _navigationManager;

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
                _navigationManager.Navigate(NavigationKeys.HelpView);
            }
            else
            {
                _navigationManager.Navigate(NavigationKeys.BuffersView);
            }
        }

        /// <summary>
        /// Хранит информацию о существующих буферах
        /// </summary>
        public ObservableCollection<BufferItem> Buffers { get; private set; }
    }
}
