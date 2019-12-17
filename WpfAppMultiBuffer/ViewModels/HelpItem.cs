using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfAppMultiBuffer.ViewModels
{
    public class HelpItem : INotifyPropertyChanged
    {
        /// <summary>
        /// Команад переключения на предыдущий текст
        /// </summary>
        public Command PreviousCommand { get; }

        /// <summary>
        /// Возникает при запросе предыдущего текста
        /// </summary>
        public event Action<HelpItem> Previous;

        /// <summary>
        /// Команад переключения на следущий текст текста
        /// </summary>
        public Command NextCommand { get; }

        /// <summary>
        /// Возникает при запросе следущего текста
        /// </summary>
        public event Action<HelpItem> Next;

        /// <summary>
        /// Хранимое значение
        /// </summary>
        string _value;

        /// <summary>
        /// Текущая отображаемая строка
        /// </summary>
        public string Value
        {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Свойство обновлено
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Функция вызова события "Свойство обновленно"
        /// </summary>
        /// <param name="propertyName">Имя вызывающего метода или свойства. Заполняется автоматически</param>
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
