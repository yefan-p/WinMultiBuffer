using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAL.Models
{
    public class BuffersModel
    {
        /// <summary>
        /// Id буфера
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id пользователя, которому принадлежит буфер
        /// </summary>
        public int User { get; set; }

        /// <summary>
        /// Клавиша копирования
        /// </summary>
        public int CopyKeyCode { get; set; }

        /// <summary>
        /// Клавиша вставки
        /// </summary>
        public int PasteKeyCode { get; set; }

        /// <summary>
        /// Имя буфера
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Значение буфера
        /// </summary>
        public string Value { get; set; }
    }
}