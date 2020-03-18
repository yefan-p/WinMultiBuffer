namespace MultiBuffer.IWebApi
{
    /// <summary>
    /// Информация о буфере, которая будет отправляться по сети
    /// </summary>
    public class WebBuffer
    {
        /// <summary>
        /// Заголовок буфера
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Клавиша, которая будет привязна к буферу
        /// </summary>
        public int Key { get; set; }

        /// <summary>
        /// Значение, которое хранит буфер
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Пользователь, которому принадлежит буфер
        /// </summary>
        public int UserId { get; set; }
    }
}
