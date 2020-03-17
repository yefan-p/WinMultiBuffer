using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiBuffer.WpfApp.Models.Interfaces;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using MultiBuffer.IWebApi;

namespace MultiBuffer.WpfApp.Models.Handlers
{
    /// <summary>
    /// Обращение к api. Получение сохраненных данных из буферов
    /// </summary>
    public class WebApiHandler
    {
        public WebApiHandler()
        {
            _httpClient.BaseAddress = new Uri("https://localhost:44324/");
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Аутентифицирует пользователя в webApi
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        /// <param name="password">Пароль</param>
        /// <returns></returns>
        public async Task AuthUser(string username, string password)
        {
            var authUser = new AuthenticateUser()
            {
                Username = username,
                Password = password
            };

            HttpResponseMessage httpResponse;
            try
            {
                httpResponse = await _httpClient.PostAsJsonAsync(_usersAddr + "auth", authUser);
            }
            catch (HttpRequestException ex)
            {
                //TODO: Выводить сообщение о недоступности сервера
                return;
            }

            if (httpResponse.IsSuccessStatusCode)
            {
                authUser = await httpResponse.Content.ReadAsAsync<AuthenticateUser>();
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + authUser.Token);
            }
            else
            {
                //TODO: Выводить сообщение с ошибкой от сервера
            }
        }

        /// <summary>
        /// Отправить новый элемент
        /// </summary>
        /// <param name="item">Элемент для сохранения в базу</param>
        public async Task CreateAsync(IBufferItem item)
        {
            var bufferWebApi = new WebBuffer()
            {
                Key = (int)item.Key,
                Name = item.Name,
                Value = item.Value
            };

            HttpResponseMessage httpResponse;
            try
            {
                httpResponse = await _httpClient.PostAsJsonAsync(_buffersAddr, bufferWebApi);
            }
            catch (HttpRequestException ex)
            {
                //TODO: Выводить сообщение о недоступности сервера
                return;
            }

            if (!httpResponse.IsSuccessStatusCode)
            {
                //TODO: Выводить сообщение с ошибкой от сервера
            }
        }

        /// <summary>
        /// Получить буфер по привязанной клавише
        /// </summary>
        /// <param name="bufferKey">Номер привязанной клавиши</param>
        /// <returns></returns>
        public async Task<WebBuffer> ReadAsync(int bufferKey)
        {
            WebBuffer bufferWebItem = null;
            HttpResponseMessage httpResponse;

            try
            {
                httpResponse = await _httpClient.GetAsync(_buffersAddr + bufferKey.ToString());
            }
            catch (HttpRequestException ex)
            {
                //TODO: Выводить сообщение о недоступности сервера
                return null;
            }

            if (httpResponse.IsSuccessStatusCode)
            {
                bufferWebItem = await httpResponse.Content.ReadAsAsync<WebBuffer>();
            }
            else
            {
                //TODO: Выводить сообщение с ошибкой от сервера
            }

            return bufferWebItem;
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
            HttpResponseMessage httpResponse;

            try
            {
                httpResponse = await _httpClient.PutAsJsonAsync(_buffersAddr + dataItem.Key.ToString(), dataItem);
            }
            catch (HttpRequestException ex)
            {
                //TODO: Выводить сообщение о недоступности сервера
                return;
            }

            if (!httpResponse.IsSuccessStatusCode)
            {
                //TODO: Выводить сообщение с ошибкой от сервера
            }
        }

        /// <summary>
        /// Удаляет указанный буфер
        /// </summary>
        /// <param name="bufferKey">Номер привязанной к буферу клавиши</param>
        /// <returns></returns>
        public async Task DeleteAsync(int bufferKey)
        {
            HttpResponseMessage httpResponse;
            try
            {
                httpResponse = await _httpClient.DeleteAsync(_buffersAddr + bufferKey.ToString());
            }
            catch (HttpRequestException ex)
            {
                //TODO: Выводить сообщение о недоступности сервера
                return;
            }

            if (!httpResponse.IsSuccessStatusCode)
            {
                //TODO: Выводить сообщение с ошибкой от сервера
            }
        }

        /// <summary>
        /// Клиент для отправки запросов
        /// </summary>
        readonly HttpClient _httpClient = new HttpClient();

        /// <summary>
        /// Адрес api для работы с буферами
        /// </summary>
        readonly string _buffersAddr = "api/buffers/";

        /// <summary>
        /// Адрес api для работы с пользователем
        /// </summary>
        readonly string _usersAddr = "api/users/";
    }
}
