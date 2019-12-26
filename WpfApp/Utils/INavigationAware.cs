namespace MultiBuffer.WpfApp.Utils
{
    public interface INavigationAware
    {
        void OnNavigatingTo(object arg);
        void OnNavigatingFrom();
    }
}