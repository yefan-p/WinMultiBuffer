namespace MultiBuffer.WpfApp.Utils
{
    public interface INavigationManager
    {
        void Navigate(string navigationKey, object arg = null);
    }
}