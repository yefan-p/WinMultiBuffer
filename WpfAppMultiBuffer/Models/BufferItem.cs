using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace WpfAppMultiBuffer.Models
{
    class BufferItem : INotifyPropertyChanged
    {
        /// <summary>
        /// Индекс элемента в коллецкии, заполняется автоматический при добавлении в BufferCollection
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// Ссылочный ключ, предоставляет доступ к значимому ключу
        /// </summary>
        public Keys RefKey { get; set; }
        /// <summary>
        /// Значимый ключ, предоставляет доступ к значению
        /// </summary>
        public Keys ValueKey { get; set; }
        /// <summary>
        /// Хранимое значение
        /// </summary>
        string _value;
        /// <summary>
        /// Значение.
        /// Оповещает о своем изменении
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
