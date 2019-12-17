namespace WpfAppMultiBuffer.Models.Interfaces
{
    public interface IHelpSwitchingController<TItem>
                        where TItem : IHelpItem
    {
        
        /// <summary>
        /// Отображаемое значение
        /// </summary>
        public TItem HelpItem { get; }
    }
}
