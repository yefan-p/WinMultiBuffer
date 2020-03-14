using Microsoft.AspNetCore.Mvc;

namespace MultiBuffer.WebApi.Utils
{
    public static class RequestResult
    {
        /// <summary>
        /// Ошибка сервера
        /// </summary>
        public static StatusCodeResult ServerError { get; set; } = new StatusCodeResult(500);

        /// <summary>
        /// Ошибка клиента
        /// </summary>
        public static StatusCodeResult ClientError { get; set; } = new StatusCodeResult(400);

        /// <summary>
        /// Запрос выполнен успешно
        /// </summary>
        public static StatusCodeResult Success { get; set; } = new StatusCodeResult(200);
    }
}
