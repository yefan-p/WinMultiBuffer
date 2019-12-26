using MultiBuffer.WpfApp.Models.Interfaces;

namespace MultiBuffer.WpfApp.ViewModels.Services
{
    public class BufferItemFactory : IBufferItemFactory
    {
        public IBufferItem GetBuffer()
        {
            return new BufferItem();
        }
    }
}