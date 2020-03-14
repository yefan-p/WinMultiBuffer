namespace MultiBuffer.IWebApi
{
    public interface IWebBuffer
    {
        /// <summary>
        /// Заголовок буфера
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Клавиша, которая будет привязна к буферу
        /// </summary>
        int Key { get; set; }

        /// <summary>
        /// Значение, которое хранит буфер
        /// </summary>
        string Value { get; set; }
    }
}
