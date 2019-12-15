using System;
using System.Collections.Generic;

namespace WpfAppMultiBuffer.Models.Interfaces
{
    public interface ICopyPasteController<TCollection, TItem>
        where TCollection : IList<TItem>
        where TItem : IBufferItem
    {

        /// <summary>
        /// Событие возникает при встваке элемента в коллекцию
        /// </summary>
        event Action<TItem> Update;

        /// <summary>
        /// Коллекция буферов
        /// </summary>
        TCollection Buffer { get; }
    }
}