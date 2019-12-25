using WpfAppMultiBuffer.Models.Interfaces;

namespace WpfAppMultiBufferTests.Mock
{
    public class BufferItemFactoryMock : IBufferItemFactory
    {
        public IBufferItem GetBuffer()
        {
            return new BufferItemMock();
        }
    }
}