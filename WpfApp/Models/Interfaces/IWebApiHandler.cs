using System.Collections.Generic;
using System.Threading.Tasks;
using MultiBuffer.IWebApi;

namespace MultiBuffer.WpfApp.Models.Interfaces
{
    public interface IWebApiHandler
    {

        /// <summary>
        /// Аутентифицирует пользователя в webApi
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        /// <param name="password">Пароль</param>
        /// <returns></returns>
        Task AuthUser(string username, string password);

        /// <summary>
        /// Отправить новый элемент
        /// </summary>
        /// <param name="item">Элемент для сохранения в базу</param>
        Task CreateAsync(IBufferItem item);

        /// <summary>
        /// Получить буфер по привязанной клавише
        /// </summary>
        /// <param name="bufferKey">Номер привязанной клавиши</param>
        /// <returns></returns>
        Task<WebBuffer> ReadAsync(int bufferKey);

        /// <summary>
        /// Получает все буферы пользователя
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<WebBuffer>> ReadListAsync();

        /// <summary>
        /// Обновляет указнный буфер
        /// </summary>
        /// <param name="item">Новый буфер</param>
        /// <returns></returns>
        Task UpdateAsync(IBufferItem item);

        /// <summary>
        /// Удаляет указанный буфер
        /// </summary>
        /// <param name="bufferKey">Номер привязанной к буферу клавиши</param>
        /// <returns></returns>
        Task DeleteAsync(int bufferKey);
    }
}
