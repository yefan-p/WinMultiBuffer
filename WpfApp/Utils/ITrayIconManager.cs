using System;

namespace MultiBuffer.WpfApp.Utils
{
    public interface ITrayIconManager
    {
        /// <summary>
        /// Добавляет пункт контексного меню для иконки в трее
        /// </summary>
        /// <param name="label">Метка, которая будет отображаться в меню</param>
        /// <param name="action">Действие, которое необходимо произвести при выборе пункта</param>
        public void AddContextMenuItem(string label, Action action);

        /// <summary>
        /// Показывает уведомление
        /// </summary>
        /// <param name="timeout">Время, которое будет отображаться уведомление</param>
        public void ShowNotify(int timeout);

        /// <summary>
        /// Скрывает уведомление
        /// </summary>
        public void HideNotify();

        /// <summary>
        /// Устанавливает команду по клике на иконку в трее
        /// </summary>
        /// <param name="action">Команда</param>
        public void AddClickCommand(Action action);
    }
}
