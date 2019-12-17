using WpfAppMultiBuffer.Utils;
using WpfAppMultiBuffer.Models.Interfaces;

namespace WpfAppMultiBuffer.ViewModels
{
    public class HelpViewModel : BaseViewModel
    {
        /// <summary>
        /// Коллекция отображаемых значений
        /// </summary>
        private string[] _values =
            {
                "Press light hot keys for activation",
                "Press any light keys for copy",
                "Press any light key for paste",
            };

        public Command NextMessage { get; }
        public Command PreviousMessage { get; }

        private int numberOfMessage = 0;

        private string helpMessage;
        public string HelpMessage
        {
            get => helpMessage;
            set
            {
                helpMessage = value;
                OnPropertyChanged();
            }
        }

        public HelpViewModel(INavigationManager navigationManager)
                            : base(navigationManager)
        {
            HelpMessage = _values[0];
            NextMessage = new Command(() =>
            {
                if (numberOfMessage == _values.Length - 1)
                {
                    numberOfMessage = 0;
                }
                else
                {
                    numberOfMessage++;
                }

                HelpMessage = _values[numberOfMessage];
            });
            PreviousMessage = new Command(() =>
            {
                if (numberOfMessage == 0)
                {
                    numberOfMessage = _values.Length - 1;
                }
                else
                {
                    numberOfMessage--;
                }

                HelpMessage = _values[numberOfMessage];
            });
        }
    }
}
