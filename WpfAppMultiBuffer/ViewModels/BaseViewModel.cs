using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfAppMultiBuffer.Utils;

namespace WpfAppMultiBuffer.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        protected INavigationManager NavigationManager { get; private set; }

        public BaseViewModel(INavigationManager navigationManager)
        {
            NavigationManager = navigationManager;
        }

        /// <summary>
        /// Свойство обновлено
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Функция вызова события "Свойство обновленно"
        /// </summary>
        /// <param name="propertyName">Имя вызывающего метода или свойства. Заполняется автоматически</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
