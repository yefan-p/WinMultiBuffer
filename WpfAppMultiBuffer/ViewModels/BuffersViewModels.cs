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
        ICopyPasteController copyPasteController;

        public BuffersViewModels(
                        INavigationManager navigationManager,
                        ICopyPasteController copyPasteController)
                        : base(navigationManager)
        {
            Buffers = new ObservableCollection<BufferItem>();

            this.copyPasteController = copyPasteController;
            copyPasteController.Update += CopyPasteController_Update;
        }

        private void CopyPasteController_Update(BufferItem obj)
        {
            var index = Buffers.IndexOf(obj);

            if(index == -1)
            {
                Buffers.Add(obj);
            }
            else
            {
                Buffers.Remove(obj);
                Buffers.Add(obj);
            }
        }

        /// <summary>
        /// Хранит информацию о существующих буферах
        /// </summary>
        public ObservableCollection<BufferItem> Buffers { get; private set; }
        /// <summary>
        /// Количество миллисекунд, которые должны пройти, прежде чем произойдет обращение к буферу обмена после нажатия клавиши.
        /// Задержка необходима для того, чтобы выделенный текст успел дойти до буфера обмена при копировании или успел вставиться при вставке.
        /// </summary>
        
    }
}
