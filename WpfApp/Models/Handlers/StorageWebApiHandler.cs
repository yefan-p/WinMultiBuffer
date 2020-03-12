using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiBuffer.WpfApp.Models.Interfaces;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MultiBuffer.WpfApp.Models.Handlers
{
    public class StorageWebApiHandler
    {
        public StorageWebApiHandler()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:44315/");
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Отправить новый элемент
        /// </summary>
        /// <param name="item"></param>
        public async void CreateAsync(IBufferItem item)
        {
            HttpResponseMessage httpResponse = await _httpClient.PostAsJsonAsync(_webApiBuffers, item);
            httpResponse.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Клиент для отправки запросов
        /// </summary>
        HttpClient _httpClient;

        /// <summary>
        /// Адрес для работы с буферами
        /// </summary>
        string _webApiBuffers = "api/Buffers/";
    }
}
