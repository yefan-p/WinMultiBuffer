using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiBuffer.WpfApp.Models.Interfaces;
using System.Net;

namespace MultiBuffer.WpfApp.Models.Handlers
{
    public class StorageWebApiHandler
    {
        string _webApiAddress = "https://localhost:44315/api/Buffers/";

        public void Create(IBufferItem item)
        {
            var request = WebRequest.Create(_webApiAddress + (int)item.Key) as HttpWebRequest;
        }
    }
}
