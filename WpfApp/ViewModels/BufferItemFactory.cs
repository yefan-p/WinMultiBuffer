using WpfAppMultiBuffer.Models.Interfaces;

namespace WpfAppMultiBuffer.ViewModels
{
    public class BufferItemFactory : IBufferItemFactory
    {
        public IBufferItem GetBuffer()
        {
            return new BufferItem();
        }
    }
}