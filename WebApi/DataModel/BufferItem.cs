using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultiBuffer.WebApi.DataModel
{
    public class BufferItem
    {
        public int Id { get; set; }

        /// <summary>
        /// Заголовок буфера
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Клавиша, которая будет привязна к буферу
        /// </summary>
        public int Key { get; set; }

        /// <summary>
        /// Значение.
        /// Оповещает о своем изменении
        /// </summary>
        public string Value { get; set; }
    }
}