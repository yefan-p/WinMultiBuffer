using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace WpfAppMultiBuffer.Models
{

    /// <summary>
    /// Предоставляет информацию о каждом отдельном буфере.
    /// </summary>
    public class BufferItem
    {

        /// <summary>
        /// Заголовок буфера
        /// </summary>
        public string Name 
        { 
            get { return ToString(); }
        }

        /// <summary>
        /// Клавиша, которая будет копировать в эту ячейку буфера
        /// </summary>
        public Keys CopyKey { get; set; }

        /// <summary>
        /// Клавиша, которая будет вставлять значений из этой ячейки буфера
        /// </summary>
        public Keys PasteKey { get; set; }

        /// <summary>
        /// Значение.
        /// Оповещает о своем изменении
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Объекты равны, если горячие клавиши у них одинаковые
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
            if (temp.CopyKey == CopyKey && temp.PasteKey == PasteKey)
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
            return $"{CopyKey} / {PasteKey}";
        }
    }
}
