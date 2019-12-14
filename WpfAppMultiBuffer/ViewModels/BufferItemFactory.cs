using WpfAppMultiBuffer.Interfaces;

namespace WpfAppMultiBuffer.ViewModels
{
    public class BufferItemFactory : IBufferItemFactory<BufferItem>
    {
        public IBufferItem GetBuffer()
        {
            return new BufferItem();
        }
    }
}