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
        /// <summary>
        /// Объекты равны, если хотя бы одна из клавиш равна
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            BufferItem temp = (BufferItem)obj;
            if (temp.CopyKey == CopyKey || temp.PasteKey == PasteKey)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Хэш код объекта, основан на строковом представлении
        /// </summary>
        /// <returns>Число</returns>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
        /// <summary>
        /// Строковое представление
        /// </summary>
        /// <returns>Строка, в которой указаны клавиша копирования и вставки</returns>
        public override string ToString()
        {
            return $"{CopyKey} / {PasteKey}";
        }
    }
}
