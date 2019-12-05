using System;
using System.Windows;
using System.Windows.Data;
using WpfAppMultiBuffer.ViewModels;
using System.Linq;

namespace WpfAppMultiBuffer.Views
{
    /// <summary>
    /// Interaction logic for BufferView.xaml
    /// </summary>
    public partial class BufferView : Window
    {
        public BufferView()
        {
            InitializeComponent();

            _buffer = new BufferViewModels();
            _input = new InputView();
            _input.CopyKeyPress += _buffer.Copy;
            _input.PasteKeyPress += _buffer.Paste;
            _input.CopyKeyPress += _input_CopyKeyPress;

            CreateControls();
        }
        /// <summary>
        /// Было выполнено копирование в буфер
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _input_CopyKeyPress(object sender, InputViewEventArgs e)
        {
            KeyboardVisibleManager();
        }
        /// <summary>
        /// Основная логика программы, содержит информацию о каждом буфере
        /// </summary>
        BufferViewModels _buffer;
        /// <summary>
        /// Предоставляет события о нажатых пользователем клавиш
        /// </summary>
        InputView _input;
        /// <summary>
        /// Создает элементы управления, в которых будет отображаться элементы каждого буфера
        /// </summary>
        void CreateControls()
        {
            foreach (var item in _buffer.Storage)
            {
                ItemBufferControl itemBuffer = new ItemBufferControl()
                {
                    Header = $"{item.CopyKey} / {item.PasteKey}",
                    Body = item.Value,
                    Index = item.Index,
                };

                Binding binding = new Binding()
                {
                    Source = item,
                    Path = new PropertyPath("Value"),
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                };
                itemBuffer.ClearClick += _buffer.Clear;
                itemBuffer.ClearClick += ItemBuffer_ClearClick;
                itemBuffer.SetBinding(ItemBufferControl.BodyProperty, binding);
                MainPanel.Children.Add(itemBuffer);
            }
        }
        /// <summary>
        /// Был выполнен клик по кнопке очисть буфер
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemBuffer_ClearClick(object sender, EventArgs e)
        {
            KeyboardVisibleManager();
        }
        /// <summary>
        /// Управляет видимостью подсказки с клавиатурой
        /// </summary>
        void KeyboardVisibleManager()
        {
            int result =
                (from el in _buffer.Storage
                 where el.Value != ""
                 select el).Count();

            if (result == 0)
            {
                //TODO : отобразить подсказку с клавиатурой
            }
            else
            {
                //TODO : скрыть подсказку с клавиатурой
            }
        }
        /// <summary>
        /// Освобождает занятые ресурсы перед выходом из программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            _input.Dispose();
        }
    }
}
