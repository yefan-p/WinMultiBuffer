using System;
using System.Collections.Generic;

namespace MultiBuffer.WpfApp.Models.Interfaces
{
    public interface ICopyPasteController<TCollection>
        where TCollection : IList<IBufferItem>
    {

        /// <summary>
        /// Событие возникает при встваке элемента в коллекцию
        /// </summary>
        event Action<IBufferItem> Update;

        /// <summary>
        /// Коллекция буферов
        /// </summary>
        TCollection Buffer { get; }
    }
}