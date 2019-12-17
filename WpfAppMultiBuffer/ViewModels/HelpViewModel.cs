using WpfAppMultiBuffer.Utils;
using WpfAppMultiBuffer.Models.Interfaces;

namespace WpfAppMultiBuffer.ViewModels
{
    public class HelpViewModel : BaseViewModel
    {
        /// <summary>
        /// Отображаемый элемент
        /// </summary>
        public HelpItem HelpItem { get; }

        public HelpViewModel(INavigationManager navigationManager,
                            IHelpSwitchingController<HelpItem> controller)
                            : base(navigationManager)
        {
            HelpItem = controller.HelpItem;
        }
    }
}
