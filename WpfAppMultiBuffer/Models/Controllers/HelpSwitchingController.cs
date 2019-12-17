using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppMultiBuffer.ViewModels;

namespace WpfAppMultiBuffer.Models.Controllers
{
    public class HelpSwitchingController
    {
        public HelpSwitchingController(HelpItem helpItem)
        {
            HelpItem = helpItem;
            HelpItem.Next += HelpItem_Next;
            HelpItem.Previous += HelpItem_Previous;

            HelpItem.IndexValue = 0;
            HelpItem.Value = _values[0];
        }

        /// <summary>
        /// Коллекция отображаемых значений
        /// </summary>
        private string[] _values =
            {
                "Press light hot keys for activation",
                "Press any light keys for copy",
                "Press any light key for paste",
            };

        /// <summary>
        /// Возникает при запросе предыдущего текста
        /// </summary>
        private void HelpItem_Previous(HelpItem obj)
        {
            if (obj.IndexValue == 0)
            {
                return;
            }

            obj.Value = _values[--obj.IndexValue];
        }

        /// <summary>
        /// Возникает при запросе следущего текста
        /// </summary>
        private void HelpItem_Next(HelpItem obj)
        {
            if (obj.IndexValue == _values.Length)
            {
                return;
            }

            obj.Value = _values[++obj.IndexValue];
        }

        /// <summary>
        /// Модель представления
        /// </summary>
        public HelpItem HelpItem { get; private set; }
    }
}
