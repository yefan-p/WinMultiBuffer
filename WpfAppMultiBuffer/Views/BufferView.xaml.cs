using System;
using System.Windows;
using System.Windows.Data;
using WpfAppMultiBuffer.ViewModels;

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

            CreateControls();
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
                itemBuffer.SetBinding(ItemBufferControl.BodyProperty, binding);
                MainPanel.Children.Add(itemBuffer);
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
