using System;
using System.Windows.Forms;

namespace WpfAppMultiBuffer.Interfaces
{
    public interface IBufferItem
    {
        /// <summary>
        /// Заголовок буфера
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Клавиша, которая будет копировать в эту ячейку буфера
        /// </summary>
        public Keys CopyKey { get; set; }

        /// <summary>
        /// Клавиша, которая будет вставлять значений из этой ячейки буфера
        /// </summary>
        public Keys PasteKey { get; set; }

        /// <summary>
        /// Возникает во время удаления элемента.
        /// </summary>
        public event Action<IBufferItem> Delete;

        /// <summary>
        /// Значение.
        /// Оповещает о своем изменении
        /// </summary>
        public string Value { get; set; }
    }
}