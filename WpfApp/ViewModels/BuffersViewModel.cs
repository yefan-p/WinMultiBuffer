using System.Collections.Generic;
using MultiBuffer.WpfApp.Models.Interfaces;
using MultiBuffer.WpfApp.Utils;

namespace MultiBuffer.WpfApp.ViewModels
{
    public class BuffersViewModel : BaseViewModel
    {

        public BuffersViewModel(ICopyPasteController<IList<IBufferItem>> copyPasteController)
        {
            ICopyPasteController<IList<IBufferItem>> _copyPasteController = copyPasteController;
            _copyPasteController.Update += CopyPasteController_Update;

            Buffers = copyPasteController.Buffer;
        }

        /// <summary>
        /// Хранит коллекцию буферов
        /// </summary>
        public IList<IBufferItem> Buffers { get; }

        /// <summary>
        /// Обрабатывает обновление коллекции
        /// </summary>
        /// <param name="obj">Буфер, который стал причиной обновления</param>
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
