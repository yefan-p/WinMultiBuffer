using System;
using System.Windows.Forms;

namespace MultiBuffer.WpfApp.Models.Interfaces
{
    public interface IBufferItem
    {
        /// <summary>
        /// Заголовок буфера
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Клавиша, которая будет копировать в эту ячейку буфера
        /// </summary>
        Keys CopyKey { get; set; }

        /// <summary>
        /// Клавиша, которая будет вставлять значений из этой ячейки буфера
        /// </summary>
        Keys PasteKey { get; set; }

        /// <summary>
        /// Возникает во время удаления элемента.
        /// </summary>
        event Action<IBufferItem> Delete;

        /// <summary>
        /// Значение.
        /// Оповещает о своем изменении
        /// </summary>
        string Value { get; set; }
    }
}