using System;
using System.Windows.Forms;
using MultiBuffer.WpfApp.Models.Interfaces;

namespace MultiBuffer.WpfApp.ViewModels.Implements
{

    /// <summary>
    /// Предоставляет информацию о каждом отдельном буфере.
    /// </summary>
    public class BufferItem : IBufferItem
    {

        public BufferItem()
        {
            DeleteCommand = new Command(() => Delete?.Invoke(this));
        }

        /// <summary>
        /// Команад удаления буфера
        /// </summary>
        public Command DeleteCommand { get; }

        /// <summary>
        /// Возникает при удалении буфера
        /// </summary>
        public event Action<IBufferItem> Delete;

        /// <summary>
        /// Заголовок буфера
        /// </summary>
        public string Name 
        { 
            get { return ToString(); }
        }

        /// <summary>
        /// Клавиша, которая будет привязна к буферу
        /// </summary>
        public Keys Key { get; set; }

        /// <summary>
        /// Значение.
        /// Оповещает о своем изменении
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Объекты равны, если привязанная клавиша равна
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (ReferenceEquals(obj, this))
            {
                return true;
            }

            if (!(obj is BufferItem))
            {
                return false;
            }

            BufferItem temp = (BufferItem)obj;
            if (temp.Key == Key)
            {
                return true;
            }
            
            return false;
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
            return $"{Key}";
        }
    }
}
