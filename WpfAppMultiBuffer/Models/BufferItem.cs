using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace WpfAppMultiBuffer.Models
{
    /// <summary>
    /// Предоставляет информацию о каждом отдельном буфере.
    /// </summary>
    public class BufferItem : INotifyPropertyChanged
    {
        /// <summary>
        /// Индекс элемента в коллецкии, заполняется автоматический при добавлении в BufferCollection
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// Клавиша, которая будет копировать в эту ячейку буфера
        /// </summary>
        public Keys CopyKey { get; set; }
        /// <summary>
        /// Клавиша, которая будет вставлять значений из этой ячейки буфера
        /// </summary>
        public Keys PasteKey { get; set; }
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
        /// Свойство обновлено. Возникает после обновления свойства
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
