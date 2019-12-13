using System;
using System.Collections.Generic;
using WpfAppMultiBuffer.Models;

namespace WpfAppMultiBuffer.Interfaces
{
    public interface ICopyPasteController<TCollection> where TCollection : IList<BufferItem>
    {
        event Action<BufferItem> Update;

        TCollection Buffer { get; }
    }
}