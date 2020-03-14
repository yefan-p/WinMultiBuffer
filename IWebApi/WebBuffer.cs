namespace MultiBuffer.IWebApi
{
    public class WebBuffer : IWebBuffer
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
    }
}
