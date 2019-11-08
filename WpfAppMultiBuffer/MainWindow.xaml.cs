using System;
using System.Windows;
using NHotkey;
using NHotkey.Wpf;
using System.Windows.Input;
using Gma.System.MouseKeyHook;

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
            HotkeyManager.Current.AddOrReplace("ActivateMultiBufferWPF", Key.OemTilde, ModifierKeys.Control, ActivateBuffer);

            IKeyboardEvents keyboardEvents;
            keyboardEvents = Hook.GlobalEvents();
            keyboardEvents.KeyDown += KeyboardEvents_KeyDown;
        }

        private void KeyboardEvents_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (_multiBuffer.IsActive)
            {
                e.SuppressKeyPress = true;
                _multiBuffer.IsActive = false;
                _multiBuffer.KeyDownManager(e.KeyCode);
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
