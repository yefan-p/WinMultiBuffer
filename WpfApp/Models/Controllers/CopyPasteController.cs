using System;
using System.Collections.Generic;
using MultiBuffer.WpfApp.Models.Interfaces;
using MultiBuffer.WpfApp.Models.Handlers;

namespace MultiBuffer.WpfApp.Models.Controllers
{
    public class CopyPasteController<TCollection>
        : ICopyPasteController<TCollection>
        where TCollection : IList<IBufferItem>
    {
        public CopyPasteController(
                    IInputHandler inputHandler,
                    TCollection collection,
                    IBufferItemFactory bufferItemFactory,
                    IClipboardController clipboardController)
        {
            Buffer = collection;
            _bufferItemFactory = bufferItemFactory;
            _clipboardController = clipboardController;

            inputHandler.PasteKeyPress += Paste;
            inputHandler.CopyKeyPress += Copy;
        }

        /// <summary>
        /// Событие возникает при встваке или удаления элемента в коллекцию
        /// </summary>
        public event Action<IBufferItem> Update;

        /// <summary>
        /// Коллекция буферов
        /// </summary>
        public TCollection Buffer { get; private set; }

        /// <summary>
        /// Предоставляет экземпляр класса BufferItem
        /// </summary>
        private readonly IBufferItemFactory _bufferItemFactory;

        private readonly IClipboardController _clipboardController;

        /// <summary>
        /// Вставляет текст из указанного буфера
        /// </summary>
        /// <param name="key">Нажатая клавиша, указывающая, из какого буфера будет вставлен текст</param>
        private void Paste(object sender, InputHandlerEventArgs key)
        {
            IBufferItem tmpItem = _bufferItemFactory.GetBuffer();
            tmpItem.Key = key.Key;

            int index = Buffer.IndexOf(tmpItem);
            if (index > -1)
            {
                _clipboardController.SetText(Buffer[index].Value);
            }
        }

        /// <summary>
        /// Копирует текст и сохраняет его в указанный буфер
        /// </summary>
        /// <param name="key">Нажатая клавиша, указывающая, в какой буфер будет вставлен текст</param>
        private void Copy(object sender, InputHandlerEventArgs key)
        {
            IBufferItem tmpItem = _bufferItemFactory.GetBuffer();
            tmpItem.Key = key.Key;
            tmpItem.Value = key.Value;

            int index = Buffer.IndexOf(tmpItem);
            if (index > -1)
            {
                Buffer[index].Value = tmpItem.Value;
            }
            else
            {
                App.Current.Dispatcher.Invoke(new Action(() =>
                {
                    Buffer.Add(tmpItem);
                    tmpItem.Delete += TmpItem_Delete;
                }));
            }

            Update?.Invoke(tmpItem);
        }

        /// <summary>
        /// Удаляет выбранный буфер
        /// </summary>
        /// <param name="obj"></param>
        private void TmpItem_Delete(IBufferItem obj)
        {
            Buffer.Remove(obj);
            Update?.Invoke(obj);
        }
    }
}
