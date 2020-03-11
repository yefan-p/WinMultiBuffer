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
        public NotifyViewModel(IShowNotifyController notifyContoller)
        {
            notifyContoller.CopyIsActive += TextNotifyContoller_UpdateProps;
            notifyContoller.PasteIsActive += TextNotifyContoller_UpdateProps;
        }

        void TextNotifyContoller_UpdateProps(string arg1, string arg2)
        {
            HeaderMessage = arg1;
            BodyMessage = arg2;
        }

        string _header;

        string _body;

        /// <summary>
        /// Загловок уведомления
        /// </summary>
        public string HeaderMessage 
        { 
            get { return _header; }
            private set
            {
                _header = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Текст уведомления
        /// </summary>
        public string BodyMessage 
        { 
            get { return _body; }
            private set
            {
                _body = value;
                OnPropertyChanged();
            }
        }
    }
}
