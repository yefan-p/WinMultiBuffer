using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiBuffer.WpfApp.Models.Interfaces;
using MultiBuffer.IWebApi;

namespace MultiBuffer.WpfApp.Models.Controllers
{
    /// <summary>
    /// Синхронизирует буферы из облака с локальными
    /// </summary>
    public class SyncBuffersController : ISyncBuffersController
    {
        public SyncBuffersController(IWebApiHandler webApi,
                                     IList<IBufferItem> buffers,
                                     IBufferItemFactory bufferItemFactory)
        {
            _buffers = buffers;
            _webApiHandler = webApi;
            _bufferFactory = bufferItemFactory;
            RunSync();
        }

        /// <summary>
        /// Событие возникает при удалении элемента из коллекции
        /// </summary>
        public event Action<IBufferItem> Update;

        /// <summary>
        /// Обращение к api
        /// </summary>
        IWebApiHandler _webApiHandler;

        /// <summary>
        /// Коллекция буферов
        /// </summary>
        IList<IBufferItem> _buffers;

        /// <summary>
        /// Предоставляет новый экземляр буфера
        /// </summary>
        IBufferItemFactory _bufferFactory;

        /// <summary>
        /// Запускает синхронизацию локальных и облачных буферов
        /// </summary>
        void RunSync()
        {
            Task task = new Task(async () =>
            {
                bool result = await _webApiHandler.AuthUser("admin", "admin"); //TODO: брать из настроек
                if (!result) return;

                var queryOld =
                        from el in _buffers
                        select new WebBuffer()
                        {
                            Key = (int)el.Key,
                            Name = el.Name,
                            Value = el.Value
                        };
                IEnumerable<WebBuffer> oldBuffers = queryOld.ToList();
                IEnumerable<WebBuffer> syncBuffers = await _webApiHandler.RefreshListAsycn(oldBuffers);

                App.Current.Dispatcher.Invoke(new Action(() => { _buffers.Clear(); }));

                foreach (WebBuffer item in syncBuffers)
                {
                    IBufferItem bufferItem = _bufferFactory.GetBuffer();
                    bufferItem.Key = (Keys)item.Key;
                    bufferItem.Value = item.Value;
                    bufferItem.Delete += BufferItem_Delete;
                    App.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        _buffers.Add(bufferItem);
                    }));
                }
            });
            task.Start();
        }

        /// <summary>
        /// Удаляет выбранный буфер
        /// </summary>
        /// <param name="obj"></param>
        void BufferItem_Delete(IBufferItem obj)
        {
            _buffers.Remove(obj);
            _webApiHandler.DeleteAsync((int)obj.Key);
            Update?.Invoke(obj);
        }
    }
}
