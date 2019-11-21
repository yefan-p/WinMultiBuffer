using System.Linq;
using WindowsInput;
using WindowsInput.Native;
using TextCopy;
using WpfAppMultiBuffer.Models;
using WpfAppMultiBuffer.Views;
using System;
using System.Windows.Threading;

namespace WpfAppMultiBuffer.ViewModels
{
    /// <summary>
    /// Основная логика работы программы, содержит коллекцию данных буфера обмена
    /// </summary>
    class BufferViewModels
    {
        public BufferViewModels()
        {
            Storage.AddRange(Literals.KeysCopy, Literals.KeysPaste, "");
        }
        /// <summary>
        /// Содержимое буфера
        /// </summary>
        public BufferCollection Storage { get; set; } = new BufferCollection();
        /// <summary>
        /// Вставляет или копирует текст, зависит от нажатой клавиши
        /// </summary>
        /// <param name="key">Нажатая клавиша</param>
        public void Update(object sender, InputViewEventArgs key)
        {
            InputSimulator inputSimulator = new InputSimulator();

            if (Literals.KeysCopy.Contains(key.InputKey))
            {
                string contentsClipboard = TextCopy.Clipboard.GetText() ?? "";
                inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LCONTROL);
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_C);
                inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);

                DispatcherTimer timer = new DispatcherTimer()
                { 
                    Interval = TimeSpan.FromMilliseconds(Literals.Interval), 
                };

                timer.Tick += (timerInner, eventArgs) =>
                {
                    timer.Stop();
                    Storage[key.InputKey] = TextCopy.Clipboard.GetText();
                    TextCopy.Clipboard.SetText(contentsClipboard);
                };

                timer.Start();
            }
            else if (Literals.KeysPaste.Contains(key.InputKey) && Storage[key.InputKey] != null && Storage[key.InputKey] != "")
            {
                string contentsClipboard = TextCopy.Clipboard.GetText() ?? "";
                TextCopy.Clipboard.SetText(Storage[key.InputKey]);
                inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LCONTROL);
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_V);
                inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);

                DispatcherTimer timer = new DispatcherTimer()
                {
                    Interval = TimeSpan.FromMilliseconds(Literals.Interval),
                };

                timer.Tick += (timerInner, eventArgs) =>
                {
                    timer.Stop();
                    TextCopy.Clipboard.SetText(contentsClipboard);
                };
                timer.Start();
            }
        }
        /// <summary>
        /// Очистить содержимое указанного буфера
        /// </summary>
        /// <param name="sender">ItemBufferControl. Буфер, который необходимо очистить</param>
        /// <param name="e"></param>
        public void Clear(object sender, EventArgs e)
        {
            ItemBufferControl itemBuffer = (ItemBufferControl)sender;
            Storage.Clear(itemBuffer.Index);
        }
    }
}
