using System.ComponentModel.DataAnnotations;

namespace MultiBuffer.WebApi.DataModels
{
    /// <summary>
    /// Хранит информацию о буфере
    /// </summary>
    public class BufferItem
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

        /// <summary>
        /// Id пользователя, которому принадлежит буфер
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Пользователь, которому принадлежит буфер
        /// </summary>
        public User User { get; set; }
    }
}