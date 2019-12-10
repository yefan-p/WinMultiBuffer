using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace WpfAppMultiBuffer.Views
{
    /// <summary>
    /// Интерфейс для одного элемента буфера
    /// </summary>
    public partial class ItemBufferControl : UserControl
    {
        public ItemBufferControl()
        {
            InitializeComponent();
            Width = 0;
            Height = 0;
            Opacity = 0;
            Clear.Click += Clear_Click;
        }
        /// <summary>
        /// Длительность анимации в миллисекундах
        /// </summary>
        const int animationTime = 200;
        /// <summary>
        /// Клик по кнопке Очистить содержимое
        /// </summary>
        public event EventHandler<EventArgs> ClearClick;
        /// <summary>
        /// Пробрасывает событие дальше
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearClick?.Invoke(this, EventArgs.Empty);
        }
        /// <summary>
        /// Свойство зависимости, необходимо для создания привязки, которая будет автоматически обновлять
        /// в интерфейсе, информацию хранящуюся в буфере
        /// </summary>
        public static readonly DependencyProperty BodyProperty =
                DependencyProperty.Register("Body",
                                             typeof(string),
                                             typeof(ItemBufferControl),
                                             new UIPropertyMetadata("",
                                                                    new PropertyChangedCallback(BodyChanged)));
        /// <summary>
        /// Событие, которое возникает после установки нового значения для свойства BodyProperty
        /// </summary>
        /// <param name="dependencyObject">ItemBufferControl. Элемент буфера, значение свойства которого обновилось</param>
        /// <param name="args">Новое и старое значения свойства</param>
        static void BodyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            ItemBufferControl itemBuffer = (ItemBufferControl)dependencyObject;

            if (args.NewValue.ToString() == "")
            {
                DoubleAnimation animation = new DoubleAnimation
                {
                    From = 1.0,
                    To = 0.0,
                    Duration = new Duration(TimeSpan.FromMilliseconds(animationTime)),
                };
                animation.Completed += (o, e) =>
                    {
                        itemBuffer.BodyItem.Text = "";
                        itemBuffer.Width = 0;
                        itemBuffer.Height = 0;
                    };
                itemBuffer.BeginAnimation(OpacityProperty, animation);
            }
            else
            {
                itemBuffer.BodyItem.Text = args.NewValue.ToString();
                itemBuffer.Width = double.NaN;
                itemBuffer.Height = double.NaN;
                DoubleAnimation animation = new DoubleAnimation
                {
                    From = 0.0,
                    To = 1.0,
                    Duration = new Duration(TimeSpan.FromMilliseconds(animationTime)),
                };
                itemBuffer.BeginAnimation(OpacityProperty, animation);
            }
        }
        /// <summary>
        /// Заголовок буфера
        /// </summary>
        public string Header
        {
            get { return HeadItem.Text; }
            set { HeadItem.Text = value; }
        }
        /// <summary>
        /// Значение буфера
        /// </summary>
        public string Body
        {
            get { return (string)GetValue(BodyProperty); }
            set { SetValue(BodyProperty, value); }
        }
        /// <summary>
        /// Адрес буфера, необходим для передачи в события очистки
        /// </summary>
        public int Index { get; set; }
    }
}
