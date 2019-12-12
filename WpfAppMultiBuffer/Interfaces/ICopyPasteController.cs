using System;
using WpfAppMultiBuffer.Models;

namespace WpfAppMultiBuffer.Interfaces
{
    public interface ICopyPasteController
    {
        event Action<BufferItem> Update;
    }
}