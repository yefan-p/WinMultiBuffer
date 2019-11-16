using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace WpfAppMultiBuffer.Models
{
    /// <summary>
    /// Коллекция данных, которая хранит информацию о буфере обмена
    /// </summary>
    class BufferCollection : ObservableCollection<BufferItem>
    {
        /// <summary>
        /// Добавляет новое содержимое в буфер
        /// </summary>
        /// <param name="copyKey">Клавиша для копирования значений</param>
        /// <param name="pasteKey">Клавиша для вставки значения</param>
        /// <param name="value">Значение</param>
        public void Add(Keys copyKey, Keys pasteKey, string value)
        {
            if (copyKey == pasteKey)
                throw new Exception("Ref key and value key must be unique.");

            BufferItem item = new BufferItem
            {
                Index = base.Count,
                CopyKey = copyKey,
                PasteKey = pasteKey,
                Value = value,
            };

            base.Add(item);
        }
        /// <summary>
        /// Добавляет массив элементов в буфер
        /// </summary>
        /// <param name="refKeys">Массив клавиш для копирования</param>
        /// <param name="valueKeys">Массив клавиш для вставки, длина должна быть равна массиву клавиш для вставки</param>
        /// <param name="value">Значение по умолчанию</param>
        public void AddRange(Keys[] refKeys, Keys[] valueKeys, string value)
        {
            if (refKeys.Length != valueKeys.Length)
                throw new Exception("Count key must be same.");

            for (int i = 0; i < refKeys.Length; i++)
            {
                Add(refKeys[i], valueKeys[i], value);
            }
        }
        /// <summary>
        /// Получить или установить значение буфера по клавише
        /// </summary>
        /// <param name="inputKey">Нажатая клавиша</param>
        /// <returns>Возвращает значение буфера</returns>
        public string this[Keys inputKey]
        {
            get
            {
                if (this is null || this.Count == 0)
                {
                    throw new ArgumentNullException("Buffer collection is null or empty");
                }
                
                string value = 
                        (from el in this
                         where el.CopyKey == inputKey || el.PasteKey == inputKey
                         select el.Value).FirstOrDefault();

                if (value == null)
                {
                    throw new InvalidOperationException("Did not find input key in buffer collection");
                }

                return value;
            }
            set
            {
                if (this is null || this.Count == 0)
                {
                    throw new ArgumentNullException("Buffer collection is null or empty");
                }

                BufferItem item =
                        (from el in this
                         where el.CopyKey == inputKey || el.PasteKey == inputKey
                         select el).FirstOrDefault();

                if (item == null)
                {
                    throw new InvalidOperationException("Did not find input key in buffer collection");
                }

                item.Value = value;
                base.SetItem(item.Index, item);
            }
        }
    }
}
