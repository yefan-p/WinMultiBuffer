using System;
using System.Windows;

namespace WpfAppMultiBuffer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        MultiBuffer _multiBuffer;

        public MainWindow()
        {
            InitializeComponent();
            _multiBuffer = new MultiBuffer();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _multiBuffer.Dispose();
        }
    }
}
