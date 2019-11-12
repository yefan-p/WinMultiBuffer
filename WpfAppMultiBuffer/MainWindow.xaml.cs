using System;
using System.Windows;
using System.Windows.Data;
using NHotkey;
using NHotkey.Wpf;
using System.Windows.Input;
using Gma.System.MouseKeyHook;
using System.Windows.Controls;

namespace WpfAppMultiBuffer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly MultiBuffer _multiBuffer;

        public MainWindow()
        {
            InitializeComponent();

            _multiBuffer = new MultiBuffer();
            CreateControlls(_multiBuffer.Storage);
            HotkeyManager.Current.AddOrReplace("ActivateMultiBufferWPF", Key.OemTilde, ModifierKeys.Control, ActivateBuffer);

            IKeyboardEvents keyboardEvents;
            keyboardEvents = Hook.GlobalEvents();
            keyboardEvents.KeyDown += KeyboardEvents_KeyDown;
        }

        private void CreateControlls(TwiceKeyDictionary<System.Windows.Forms.Keys, string> storage)
        {
            foreach (TwiceKeyDictionaryItem<System.Windows.Forms.Keys, string> item in storage)
            {
                ItemBufferControl itemBuffer = new ItemBufferControl()
                {
                    Header = $"{item.FirtsKey} / {item.SecondKey}",
                    Body = item.Value,
                    Name = item.FirtsKey.ToString(),
                };

                Binding b = new Binding()
                {
                    Source = itemBuffer,
                    Path = new PropertyPath("Value"),
                };

                itemBuffer.SetBinding(itemBuffer.BodyItem.Text, b);
                UniformGrid.Children.Add(itemBuffer);
            }            
        }

        private void KeyboardEvents_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (_multiBuffer.IsActive)
            {
                e.SuppressKeyPress = true;
                _multiBuffer.IsActive = false;
                _multiBuffer.KeyDownManager(e.KeyCode, this);
            }
        }

        void ActivateBuffer(object sender, HotkeyEventArgs e)
        {
            _multiBuffer.IsActive = true;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            HotkeyManager.Current.Remove("ActivateMultiBufferWPF");
        }
    }
}
