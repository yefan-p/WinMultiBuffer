using WpfAppMultiBuffer.Models.Interfaces;

namespace WpfAppMultiBuffer.Models.Controllers
{
    public class HelpSwitchingController<TItem> 
                    : IHelpSwitchingController<TItem>
                    where TItem : IHelpItem
    {
        public HelpSwitchingController(TItem helpItem)
        {
            HelpItem = helpItem;
            HelpItem.Next += HelpItem_Next;
            HelpItem.Previous += HelpItem_Previous;

            HelpItem.IndexValue = 0;
            HelpItem.Value = _values[0];
        }

        /// <summary>
        /// Коллекция отображаемых значений
        /// </summary>
        private string[] _values =
            {
                "Press light hot keys for activation",
                "Press any light keys for copy",
                "Press any light key for paste",
            };

        /// <summary>
        /// Возникает при запросе предыдущего текста
        /// </summary>
        private void HelpItem_Previous(IHelpItem obj)
        {
            if (obj.IndexValue == 0)
            {
                return;
            }

            obj.Value = _values[--obj.IndexValue];
        }

        /// <summary>
        /// Возникает при запросе следущего текста
        /// </summary>
        private void HelpItem_Next(IHelpItem obj)
        {
            if (obj.IndexValue == _values.Length - 1)
            {
                return;
            }

            obj.Value = _values[++obj.IndexValue];
        }

        /// <summary>
        /// Отображаемое значение
        /// </summary>
        public TItem HelpItem { get; private set; }
    }
}
