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
            Clear.Click += Clear_Click;
            //SizeChanged += ItemBufferControl_SizeChanged;
        }
        bool animationWidthPlay = false;
        bool animationHeightPlay = false;
        /// <summary>
        /// Отображает анимацию разворачивания и скрытия блока
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemBufferControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!animationWidthPlay && !animationHeightPlay)
            {
                if (e.HeightChanged && e.PreviousSize.Height < e.NewSize.Height)
                {
                    e.Handled = true;
                    animationHeightPlay = true;
                    HideAnimation(e.PreviousSize.Height, e.NewSize.Height, HeightProperty, ref animationHeightPlay);
                }
                else if(e.HeightChanged && e.PreviousSize.Height > e.NewSize.Height)
                {
                    e.Handled = true;
                    animationHeightPlay = true;
                    HideAnimation(e.NewSize.Height, e.PreviousSize.Height, HeightProperty, ref animationHeightPlay);
                }

                if (e.WidthChanged && e.PreviousSize.Width < e.NewSize.Width)
                {
                    //e.Handled = true;
                    animationWidthPlay = true;
                    HideAnimation(e.PreviousSize.Width, e.NewSize.Width, WidthProperty, ref animationWidthPlay);
                }
                else if(e.WidthChanged && e.PreviousSize.Width > e.NewSize.Width)
                {
                    e.Handled = true;
                    animationWidthPlay = true;
                    HideAnimation(e.NewSize.Width, e.PreviousSize.Width, WidthProperty, ref animationWidthPlay);
                }
            }
        }
        /// <summary>
        /// Воспроизвести анимацию для заданного свойства
        /// </summary>
        /// <param name="from">Начальное значение</param>
        /// <param name="to">Конечное значение</param>
        /// <param name="property">Свойство, которое будет меняться</param>
        /// <param name="playFlag">Флаг, указывающий, что анимация воспроизводиться</param>
        void HideAnimation(double from, double to, DependencyProperty property, ref bool playFlag)
        {
            bool flag = playFlag;
            DoubleAnimation animation = new DoubleAnimation
            {
                From = from,
                To = to,
                Duration = new Duration(TimeSpan.FromMilliseconds(500)),
            };
            animation.Completed += (o, e) => { flag = false; };
            playFlag = flag;
            BeginAnimation(property, animation);
        }
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
            itemBuffer.BodyItem.Text = args.NewValue.ToString();

            if (itemBuffer.BodyItem.Text == "")
            {
                itemBuffer.Width = 0;
                itemBuffer.Height = 0;
            }
            else
            {
                itemBuffer.Width = double.NaN;
                itemBuffer.Height = double.NaN;
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
