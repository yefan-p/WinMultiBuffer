using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppMultiBuffer.Views
{
    public partial class ItemBufferControl : UserControl
    {
        public static readonly DependencyProperty BodyProperty =
                DependencyProperty.Register("Body",
                                             typeof(string),
                                             typeof(ItemBufferControl),
                                             new UIPropertyMetadata("",
                                                                    new PropertyChangedCallback(BodyChanged)));

        static void BodyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            ItemBufferControl itemBuffer = (ItemBufferControl)dependencyObject;
            itemBuffer.BodyItem.Text = args.NewValue.ToString();

            if (itemBuffer.BodyItem.Text == "")
            {
                itemBuffer.Width = 0;
            }
            else
            {
                itemBuffer.Width = double.NaN;
            }
        }

        public string Header
        {
            get { return HeadItem.Text; }
            set { HeadItem.Text = value; }
        }

        public string Body
        {
            get { return (string)GetValue(BodyProperty); }
            set { SetValue(BodyProperty, value); }
        }

        public ItemBufferControl()
        {
            InitializeComponent();
            Width = 0;
        }
    }
}
