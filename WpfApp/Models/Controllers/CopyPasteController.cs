using System;
using System.Collections.Generic;
using MultiBuffer.WpfApp.Models.Interfaces;
using MultiBuffer.WpfApp.Models.Handlers;
using TextCopy;

namespace MultiBuffer.WpfApp.Models.Controllers
{
    public class CopyPasteController<TCollection> : ICopyPasteController<TCollection>
                 where TCollection : IList<IBufferItem>
    {
        public CopyPasteController(
                    IInputHandler inputHandler,
                    TCollection collection,
                    IBufferItemFactory bufferItemFactory,
                    IWebApiHandler webHandler)
        {
            Buffer = collection;
            _bufferItemFactory = bufferItemFactory;
            _webHandler = webHandler;
            _webHandler.AuthUser("admin", "admin");//TODO: убрать 

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
        readonly IBufferItemFactory _bufferItemFactory;

        /// <summary>
        /// Добавляет, читает, обновляет, удаляет буфферы в облаке
        /// </summary>
        readonly IWebApiHandler _webHandler;

        /// <summary>
        /// Вставляет текст из указанного буфера
        /// </summary>
        /// <param name="key">Нажатая клавиша, указывающая, из какого буфера будет вставлен текст</param>
        void Paste(object sender, InputHandlerEventArgs key)
        {
            IBufferItem tmpItem = _bufferItemFactory.GetBuffer();
            tmpItem.Key = key.Key;

            int index = Buffer.IndexOf(tmpItem);
            if (index > -1)
            {
                Clipboard.SetText(Buffer[index].Value);
            }
        }

        /// <summary>
        /// Копирует текст и сохраняет его в указанный буфер
        /// </summary>
        /// <param name="key">Нажатая клавиша, указывающая, в какой буфер будет вставлен текст</param>
        void Copy(object sender, InputHandlerEventArgs key)
        {
            IBufferItem tmpItem = _bufferItemFactory.GetBuffer();
            tmpItem.Key = key.Key;
            tmpItem.Value = key.Value;

            int index = Buffer.IndexOf(tmpItem);
            if (index > -1)
            {
                Buffer[index].Value = tmpItem.Value;
                _webHandler.UpdateAsync(tmpItem);
            }
            else
            {
                App.Current.Dispatcher.Invoke(new Action(() =>
                {
                    Buffer.Add(tmpItem);
                    _webHandler.CreateAsync(tmpItem);
                    tmpItem.Delete += TmpItem_Delete;
                }));
            }

            Update?.Invoke(tmpItem);
        }

        /// <summary>
        /// Удаляет выбранный буфер
        /// </summary>
        /// <param name="obj"></param>
        void TmpItem_Delete(IBufferItem obj)
        {
            Buffer.Remove(obj);
            _webHandler.DeleteAsync((int)obj.Key);
            Update?.Invoke(obj);
        }
    }
}
