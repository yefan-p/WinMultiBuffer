using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiBuffer.WpfApp.Models.Interfaces;
using MultiBuffer.IWebApi;
using System.Diagnostics;

namespace MultiBuffer.WpfApp.Models.Controllers
{
    /// <summary>
    /// Синхронизирует буферы из облака с локальными
    /// </summary>
    public class SyncBuffersController : ISyncBuffersController
    {
        public SyncBuffersController(IWebApiHandler webApi)
        {
            RunSynchronization(webApi);
        }

        void RunSynchronization(IWebApiHandler webApi)
        {
            Task task = new Task(async () =>
            {
                await webApi.AuthUser("admin", "admin"); //TODO: брать из настроек
                IEnumerable<WebBuffer> webBuffers = await webApi.ReadListAsync();
            });
            task.Start();
        }

    }
}
