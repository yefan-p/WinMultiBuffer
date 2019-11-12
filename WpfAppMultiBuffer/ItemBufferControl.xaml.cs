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

namespace WpfAppMultiBuffer
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ItemBufferControl : UserControl
    {
        public static readonly DependencyProperty BodyProperty;

        public string Header 
        {
            get { return HeadItem.Text; } 
            set { HeadItem.Text = value; }
        }

        public string Body 
        { 
            get { return BodyItem.Text; }
            set { BodyItem.Text = value; }
        }

        public ItemBufferControl()
        {
            InitializeComponent();
        }
    }
}
