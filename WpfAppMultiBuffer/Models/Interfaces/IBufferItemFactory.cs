namespace WpfAppMultiBuffer.Models.Interfaces
{
    public interface IBufferItemFactory<TItem> where TItem : IBufferItem
    {
        IBufferItem GetBuffer();
    }
}
