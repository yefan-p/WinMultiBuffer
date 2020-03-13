using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MultiBuffer.WebApiCore.DataModels
{
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
    }
}