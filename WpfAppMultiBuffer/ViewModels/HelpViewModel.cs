using WpfAppMultiBuffer.Utils;
using WpfAppMultiBuffer.Models.Controllers;

namespace WpfAppMultiBuffer.ViewModels
{
    public class HelpViewModel : BaseViewModel
    {
        /// <summary>
        /// Отображаемый элемент
        /// </summary>
        public HelpItem HelpItem { get; }

        public HelpViewModel(INavigationManager navigationManager,
                            HelpSwitchingController controller)
                            : base(navigationManager)
        {
            HelpItem= controller.HelpItem;
        }
    }
}
