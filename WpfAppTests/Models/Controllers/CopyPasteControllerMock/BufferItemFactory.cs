using MultiBuffer.WpfApp.Models.Interfaces;

namespace MultiBuffer.WpfAppTests.Models.Controllers.CopyPasteControllerMock
{
    public class BufferItemFactory : IBufferItemFactory
    {
        public IBufferItem GetBuffer()
        {
            return new BufferItem();
        }
    }
}