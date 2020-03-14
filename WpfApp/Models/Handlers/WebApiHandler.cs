using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiBuffer.WpfApp.Models.Interfaces;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;
using MultiBuffer.IWebApi;

namespace MultiBuffer.WpfApp.Models.Handlers
{
    public class WebApiHandler
    {
        public WebApiHandler(IBufferItemFactory bufferFactory)
        {
            _bufferFactory = bufferFactory;
            _httpClient.BaseAddress = new Uri("https://localhost:44324/api/buffers/");
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Отправить новый элемент
        /// </summary>
        /// <param name="item">Элемент для сохранения в базу</param>
        public async Task CreateAsync(IBufferItem item)
        {
            WebBuffer bufferWebApi = new WebBuffer()
            {
                Key = (int)item.Key,
                Name = item.Name,
                Value = item.Value
            };

            HttpResponseMessage httpResponse = await _httpClient.PostAsJsonAsync("", bufferWebApi);
        }

        /// <summary>
        /// Получить буфер по привязанной клавише
        /// </summary>
        /// <param name="bufferKey">Номер привязанной клавиши</param>
        /// <returns></returns>
        public async Task<IBufferItem> ReadAsync(int bufferKey)
        {
            WebBuffer bufferWebItem = null;
            HttpResponseMessage httpResponse = await _httpClient.GetAsync(bufferKey.ToString());
            if (httpResponse.IsSuccessStatusCode)
            {
                bufferWebItem = await httpResponse.Content.ReadAsAsync<WebBuffer>();
            }

            IBufferItem bufferItem = _bufferFactory.GetBuffer();
            bufferItem.Key = (Keys)bufferWebItem.Key;
            bufferItem.Value = bufferWebItem.Value;

            return bufferItem;
        }

        /// <summary>
        /// Обновляет указнный буфер
        /// </summary>
        /// <param name="item">Новый буфер</param>
        /// <returns></returns>
        public async Task UpdateAsync(IBufferItem item)
        {
            var dataItem = new WebBuffer
            {
                Name = item.Name,
                Value = item.Value,
                Key = (int)item.Key
            };
            HttpResponseMessage httpResponse = await _httpClient.PutAsJsonAsync(dataItem.Key.ToString(), dataItem);
        }

        /// <summary>
        /// Удаляет указанный буфер
        /// </summary>
        /// <param name="bufferKey">Номер привязанной к буферу клавиши</param>
        /// <returns></returns>
        public async Task DeleteAsync(int bufferKey)
        {
            HttpResponseMessage httpResponse = await _httpClient.DeleteAsync(bufferKey.ToString());
        }

        /// <summary>
        /// Клиент для отправки запросов
        /// </summary>
        HttpClient _httpClient = new HttpClient();

        /// <summary>
        /// Фабрика буферов
        /// </summary>
        IBufferItemFactory _bufferFactory;
    }
}
