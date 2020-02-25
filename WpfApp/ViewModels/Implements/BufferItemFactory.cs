using MultiBuffer.WpfApp.Models.Interfaces;

namespace MultiBuffer.WpfApp.ViewModels.Implements
{
    public class BufferItemFactory : IBufferItemFactory
    {
        public IBufferItem GetBuffer()
        {
            return new BufferItem();
        }
    }
}