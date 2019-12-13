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
    public class BuffersViewModels : BaseViewModel
    {
        readonly ICopyPasteController _copyPasteController;

        public BuffersViewModels(
                        INavigationManager navigationManager,
                        ICopyPasteController copyPasteController)
                        : base(navigationManager)
        {
            Buffers = new ObservableCollection<BufferItem>();

            _copyPasteController = copyPasteController;
            copyPasteController.Update += CopyPasteController_Update;
        }

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
        }

        /// <summary>
        /// Хранит информацию о существующих буферах
        /// </summary>
        public ObservableCollection<BufferItem> Buffers { get; private set; }
    }
}
