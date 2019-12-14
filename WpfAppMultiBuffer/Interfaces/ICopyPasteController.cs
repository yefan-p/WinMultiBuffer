using System;
using System.Collections.Generic;
using WpfAppMultiBuffer.Models;

namespace WpfAppMultiBuffer.Interfaces
{
    public interface ICopyPasteController<TCollection> where TCollection : IList<BufferItem>
    {

        /// <summary>
        /// Событие возникает при встваке элемента в коллекцию
        /// </summary>
        event Action<BufferItem> Update;

        /// <summary>
        /// Коллекция буферов
        /// </summary>
        TCollection Buffer { get; }
    }
}