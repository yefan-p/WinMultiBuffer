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
using NHotkey;
using NHotkey.Wpf;
using WindowsInput;
using Gma.System.MouseKeyHook;
using WindowsInput.Native;

namespace WpfAppMultiBuffer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TwiceKeyDictionary<Key, string> multiBuffer = new TwiceKeyDictionary<Key, string>();

        Key[] keyCopy =
            {
                Key.D1, Key.D2, Key.D3, Key.D4, Key.D5, Key.D6, Key.D7, Key.D8, Key.D9, Key.D0, Key.OemMinus, Key.OemPlus,
                Key.A, Key.S, /*Key.D, Key.F,*/ Key.G, Key.H, Key.J, Key.K, Key.L
            };

        Key[] keyPaste =
            {
                Key.Q, Key.W, Key.E, Key.R, Key.T, Key.Y, Key.U, Key.I, Key.O, Key.P, Key.OemOpenBrackets, Key.Oem6, 
                Key.Z, Key.X, /*Key.C, Key.V,*/ Key.B, Key.N, Key.M, Key.OemComma, Key.OemPeriod
            };

        IKeyboardEvents keyboardEvents;
        
        bool bufferIsActive = false;

        public MainWindow()
        {
            InitializeComponent();

            for (int i = 0; i < keyCopy.Length; i++)
            {
                multiBuffer.Add(keyCopy[i], keyPaste[i], i.ToString());
            }

            HotkeyManager.Current.AddOrReplace("ActivateMultiBuffer", Key.OemTilde, ModifierKeys.Control, ActivateBuffer);

            keyboardEvents = Hook.GlobalEvents();
            keyboardEvents.KeyPress += KeyboardEvents_KeyPress;
        }

        private void KeyboardEvents_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (bufferIsActive)
            {
                bufferIsActive = false;
                InputSimulator inputSimulator = new InputSimulator();
                Key key = ParseChar.KeyEnum(e.KeyChar);

                if (keyCopy.Contains(key))
                {
                    //string tmpClipboard = Clipboard.GetDataObject();

                    inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LCONTROL);
                    inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_C);
                    inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);
                    inputSimulator.Keyboard.Sleep(500);

                    multiBuffer[key] = Clipboard.GetText(TextDataFormat.UnicodeText);

                    //Clipboard.SetDataObject(tmpClipboard);
                }
                else if (keyPaste.Contains(key))
                {
                    //object tmpClipboard = Clipboard.GetDataObject();
                    Clipboard.SetText(multiBuffer[key]);

                    inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LCONTROL);
                    inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_V);
                    inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);
                    inputSimulator.Keyboard.Sleep(500);
                    inputSimulator.Keyboard.KeyPress(VirtualKeyCode.BACK);

                    //Clipboard.SetDataObject(tmpClipboard);
                }
            }
        }

        private void ActivateBuffer(object sender, HotkeyEventArgs e)
        {
            bufferIsActive = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            HotkeyManager.Current.Remove("ActivateMultiBuffer");
        }
    }
}
