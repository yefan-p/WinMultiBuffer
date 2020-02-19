using System;
using System.Windows.Forms;
using MultiBuffer.WpfApp.Models.Interfaces;

namespace MultiBuffer.WpfAppTests.Models.Controllers.CopyPasteControllerMock
{

    /// <summary>
    /// Предоставляет информацию о каждом отдельном буфере.
    /// </summary>
    public class BufferItem : IBufferItem
    {

        /// <summary>
        /// Возникает при удалении буфера
        /// </summary>
        public event Action<IBufferItem> Delete;

        /// <summary>
        /// Заголовок буфера
        /// </summary>
        public string Name 
        { 
            get { return ToString(); }
        }

        /// <summary>
        /// Значение.
        /// Оповещает о своем изменении
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Клавиша, которая будет привязна к буферу
        /// </summary>
        public Keys Key { get; set; }
    }
}
