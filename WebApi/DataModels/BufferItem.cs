using System.ComponentModel.DataAnnotations;
using MultiBuffer.IWebApi;

namespace MultiBuffer.WebApi.DataModels
{
    public class BufferItem : IWebBuffer
    {
        public int Id { get; set; }

        /// <summary>
        /// Заголовок буфера
        /// </summary>
        [MaxLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Клавиша, которая будет привязна к буферу
        /// </summary>
        [Required]
        public int Key { get; set; }

        /// <summary>
        /// Значение? которое хранит буфер
        /// </summary>
        public string Value { get; set; }
    }
}