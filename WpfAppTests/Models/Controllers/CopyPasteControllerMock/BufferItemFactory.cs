using WpfAppMultiBuffer.Models.Interfaces;

namespace MultiBuffer.WpfAppTests.Models.Controllers.CopyPasteControllerTestsMock
{
    public class BufferItemFactory : IBufferItemFactory
    {
        public IBufferItem GetBuffer()
        {
            return new BufferItemMock();
        }
    }
}