using MultiBuffer.WpfApp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiBuffer.WpfApp.ViewModels
{
    public class NotifyViewModel : BaseViewModel
    {
        public NotifyViewModel(IShowNotifyController showNotifyController)
        {
            HeaderMessage = showNotifyController.HeaderNotifyMessage;
            BodyMessage = showNotifyController.BodyNotifyMessage;
        }

        /// <summary>
        /// Загловок уведомления
        /// </summary>
        public string HeaderMessage { get; }

        /// <summary>
        /// Текст уведомления
        /// </summary>
        public string BodyMessage { get; }
    }
}
