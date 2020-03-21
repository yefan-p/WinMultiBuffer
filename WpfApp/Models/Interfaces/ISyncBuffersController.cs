using System;

namespace MultiBuffer.WpfApp.Models.Interfaces
{
    /// <summary>
    /// Синхронизирует буферы из облака с локальными
    /// </summary>
    public interface ISyncBuffersController
    {

        /// <summary>
        /// Событие возникает при удалении элемента из коллекции
        /// </summary>
        public event Action<IBufferItem> Update;
    }
}
