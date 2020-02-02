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
        /// Клавиша, которая будет привязна к буферу
        /// </summary>
        Keys Key { get; set; }

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