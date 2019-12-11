using System;
using System.Collections.ObjectModel;
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
        public BuffersViewModels(
                        INavigationManager navigationManager,
                        InputController inputController)
                        : base(navigationManager)
        {
            Buffers = new ObservableCollection<BufferItem>();

            inputController.PasteKeyPress += Paste;
            inputController.CopyKeyPress += Copy;
        }
        /// <summary>
        /// Хранит информацию о существующих буферах
        /// </summary>
        public ObservableCollection<BufferItem> Buffers { get; private set; }
        /// <summary>
        /// Количество миллисекунд, которые должны пройти, прежде чем произойдет обращение к буферу обмена после нажатия клавиши.
        /// Задержка необходима для того, чтобы выделенный текст успел дойти до буфера обмена при копировании или успел вставиться при вставке.
        /// </summary>
        const int Interval = 250;
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
        /// <summary>
        /// Копирует текст в указанный буфер
        /// </summary>
        /// <param name="key">Нажатая клавиша, указывающая, в какой буфер будет вставлен текст</param>
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
