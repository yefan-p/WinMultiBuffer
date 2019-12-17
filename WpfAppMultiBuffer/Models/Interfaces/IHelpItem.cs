using System;

namespace WpfAppMultiBuffer.Models.Interfaces
{
    public interface IHelpItem
    {
        /// <summary>
        /// Указывает номер изменяемого значения
        /// </summary>
        public int IndexValue { get; set; }

        /// <summary>
        /// Возникает при запросе предыдущего текста
        /// </summary>
        public event Action<IHelpItem> Previous;

        /// <summary>
        /// Возникает при запросе следущего текста
        /// </summary>
        public event Action<IHelpItem> Next;

        /// <summary>
        /// Текущая отображаемая строка
        /// </summary>
        public string Value { get; set; }
    }
}
