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
using System.Windows.Shapes;
using WpfAppMultiBuffer.ViewModels;

namespace WpfAppMultiBuffer.Views
{
    /// <summary>
    /// Interaction logic for BufferView.xaml
    /// </summary>
    public partial class BufferView : Window
    {
        BufferViewModels _buffer;
        public BufferView()
        {
            InitializeComponent();

            _buffer = new BufferViewModels();
            InputView input = new InputView(_buffer);
            CreateControls();
        }

        void CreateControls()
        {
            foreach (var item in _buffer.Storage)
            {
                ItemBufferControl itemBuffer = new ItemBufferControl()
                {
                    Header = $"{item.RefKey} / {item.ValueKey}",
                    Body = item.Value,
                };

                Binding binding = new Binding()
                {
                    Source = _buffer.Storage,
                    Path = new PropertyPath("Value"),
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                };

                itemBuffer.SetBinding(ItemBufferControl.BodyProperty, binding);
                UniformGrid.Children.Add(itemBuffer);
            }
        }
    }
}
