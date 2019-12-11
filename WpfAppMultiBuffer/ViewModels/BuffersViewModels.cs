using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;
using WindowsInput;
using WindowsInput.Native;
using WpfAppMultiBuffer.Controllers;
using WpfAppMultiBuffer.Models;
using WpfAppMultiBuffer.Utils;
using WpfAppMultiBuffer.Views;

namespace WpfAppMultiBuffer.ViewModels
{
    public class BuffersViewModels : BaseViewModel
    {
        public ObservableCollection<BufferItem> Buffers { get; private set; }

        /// <summary>
        /// Количество миллисекунд, которые должны пройти, прежде чем произойдет обращение к буферу обмена после нажатия клавиши.
        /// Задержка необходима для того, чтобы выделенный текст успел дойти до буфера обмена при копировании или успел вставиться при вставке.
        /// </summary>
        const int Interval = 250;

        public BuffersViewModels(
                        INavigationManager navigationManager,
                        InputController inputController)
                        : base(navigationManager)
        {
            Buffers = new ObservableCollection<BufferItem>();

            inputController.PasteKeyPress += Paste;
            inputController.CopyKeyPress += Copy;

            //AddRange(InputController.KeysCopy, InputController.KeysPaste, "");
        }

        /// <summary>
        /// Вставляет текст из указанного буфера
        /// </summary>
        /// <param name="key">Нажатая клавиша, указывающая, из какого буфера будет вставлен текст</param>
        public void Paste(object sender, InputViewEventArgs key)
        {
            InputSimulator inputSimulator = new InputSimulator();
            string contentsClipboard = TextCopy.Clipboard.GetText() ?? "";

            foreach (var item in Buffers)
            {
                if (item.CopyKey == key.InputKey || item.PasteKey == key.InputKey)
                {
                    TextCopy.Clipboard.SetText(item.Value);
                    break;
                }
            }

            inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LCONTROL);
            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_V);
            inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);

            DispatcherTimer timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(Interval),
            };

            timer.Tick += (timerInner, eventArgs) =>
            {
                timer.Stop();
                TextCopy.Clipboard.SetText(contentsClipboard);
            };
            timer.Start();
        }

        public void CreateNewItem()
        {

        }

        public void AddRange(Keys[] refKeys, Keys[] valueKeys, string value)
        {
            for (int i = 0; i < refKeys.Length; i++)
            {
                BufferItem item = new BufferItem
                {
                    Index = i,
                    CopyKey = refKeys[i],
                    PasteKey = valueKeys[i],
                    Value = string.Empty,
                };

                Buffers.Add(item);
            }
        }

        public void Copy(object sender, InputViewEventArgs key)
        {
            InputSimulator inputSimulator = new InputSimulator();
            string contentsClipboard = TextCopy.Clipboard.GetText() ?? "";
            inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LCONTROL);
            inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_C);
            inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);

            DispatcherTimer timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(Interval),
            };

            timer.Tick += (timerInner, eventArgs) =>
            {
                timer.Stop();

                bool isInstance = false;
                foreach (var item in Buffers)
                {
                    if (item.CopyKey == key.InputKey || item.PasteKey == key.InputKey)
                    {
                        isInstance = true;
                        item.Value = TextCopy.Clipboard.GetText();
                        break;
                    }
                }

                if (!isInstance)
                {
                    var item = new BufferItem()
                    {
                        CopyKey = InputController.GetCopyKey(key.InputKey),
                        PasteKey = InputController.GetKey(key.InputKey),
                        Value = TextCopy.Clipboard.GetText()
                    };
                    Buffers.Add(item);
                }

                TextCopy.Clipboard.SetText(contentsClipboard);
            };

            timer.Start();
        }
    }
}
