using WpfAppMultiBuffer.Utils;

namespace WpfAppMultiBuffer.ViewModels
{
    public class HelpViewModel : BaseViewModel
    {
        public HelpViewModel(INavigationManager navigationManager)
                    : base(navigationManager)
        {
            HelpMessage = _values[0];
            NextMessage = new Command(() =>
            {
                if (_numberOfMessage == _values.Length - 1)
                {
                    _numberOfMessage = 0;
                }
                else
                {
                    _numberOfMessage++;
                }

                HelpMessage = _values[_numberOfMessage];
                OnPropertyChanged("NumberOfMessage");
            });
            PreviousMessage = new Command(() =>
            {
                if (_numberOfMessage == 0)
                {
                    _numberOfMessage = _values.Length - 1;
                }
                else
                {
                    _numberOfMessage--;
                }

                HelpMessage = _values[_numberOfMessage];
                OnPropertyChanged("NumberOfMessage");
            });
        }

        /// <summary>
        /// Коллекция отображаемых значений
        /// </summary>
        private string[] _values =
            {
                "Press light hot keys for activation copy or paste",
                "Press any light keys for copy",
                "Press any light key for paste",
            };

        /// <summary>
        /// Переключить на предыдущую подсказку
        /// </summary>
        public Command NextMessage { get; }

        /// <summary>
        /// Переключить на следующую подсказку
        /// </summary>
        public Command PreviousMessage { get; }

        /// <summary>
        /// Индекс текущей подсказки в массиве
        /// </summary>
        private int _numberOfMessage = 0;

        public int NumberOfMessage
        {
            get => _numberOfMessage;
            private set
            {
                _numberOfMessage = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Текущая отображаемая подсказка
        /// </summary>
        private string _helpMessage;

        /// <summary>
        /// Текущая отображаемая подсказка
        /// </summary>
        public string HelpMessage
        {
            get => _helpMessage;
            set
            {
                _helpMessage = value;
                OnPropertyChanged();
            }
        }
    }
}
