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

            Task task = new Task(async () =>
            {
                await webApi.AuthUser("admin", "admin"); //TODO: брать из настроек
                IEnumerable<WebBuffer> webBuffers = await webApi.ReadListAsync();

                foreach (WebBuffer item in webBuffers)
                {
                    IBufferItem bufferItem = bufferItemFactory.GetBuffer();
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
        /// Обращение к api
        /// </summary>
        IWebApiHandler _webApiHandler;

        /// <summary>
        /// Коллекция буферов
        /// </summary>
        IList<IBufferItem> _buffers;

        /// <summary>
        /// Удаляет выбранный буфер
        /// </summary>
        /// <param name="obj"></param>
        void BufferItem_Delete(IBufferItem obj)
        {
            _buffers.Remove(obj);
            _webApiHandler.DeleteAsync((int)obj.Key);
        }
    }
}
