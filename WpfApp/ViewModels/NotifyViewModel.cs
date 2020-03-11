using MultiBuffer.WpfApp.Models.Interfaces;

namespace MultiBuffer.WpfApp.ViewModels
{
    public class NotifyViewModel : BaseViewModel
    {
        public NotifyViewModel(IShowNotifyController notifyContoller)
        {
            notifyContoller.CopyIsActive += TextNotifyContoller_UpdateProps;
            notifyContoller.PasteIsActive += TextNotifyContoller_UpdateProps;
        }

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

        /// <summary>
        /// Обработчик событий активации копирования или вставки
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        void TextNotifyContoller_UpdateProps(string arg1, string arg2)
        {
            HeaderMessage = arg1;
            BodyMessage = arg2;
        }

        /// <summary>
        /// Загловок сообщения
        /// </summary>
        string _header;

        /// <summary>
        /// Текст сообщения
        /// </summary>
        string _body;
    }
}
