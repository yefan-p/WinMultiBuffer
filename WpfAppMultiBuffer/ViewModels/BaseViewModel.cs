using WpfAppMultiBuffer.Utils;

namespace WpfAppMultiBuffer.ViewModels
{
    public class BaseViewModel
    {
        protected INavigationManager NavigationManager { get; private set; }
        public BaseViewModel(INavigationManager navigationManager)
        {
            NavigationManager = navigationManager;
        }
    }
}
