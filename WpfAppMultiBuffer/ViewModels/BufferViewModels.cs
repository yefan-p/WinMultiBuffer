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
            Storage.AddRange(InputView.KeysCopy, InputView.KeysPaste, "");
        }
        /// <summary>
        /// Количество миллисекунд, которые должны пройти, прежде чем произойдет обращение к буферу обмена после нажатия клавиши.
        /// Задержка необходима для того, чтобы выделенный текст успел дойти до буфера обмена при копировании или успел вставиться при вставке.
        /// </summary>
        const int Interval = 250;
        /// <summary>
        /// Содержимое буфера
        /// </summary>
        public BufferCollection Storage { get; set; } = new BufferCollection();
        /// <summary>
        /// Копирует выделенный текст
        /// </summary>
        /// <param name="key">Нажатая клавиша, указывающая, в какой буфер будет положен текст</param>
        public void Copy(object sender, InputViewEventArgs key)
        {
            InputSimulator inputSimulator = new InputSimulator();
            string contentsClipboard = Clipboard.GetText() ?? "";
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
                Storage[key.InputKey] = Clipboard.GetText();
                Clipboard.SetText(contentsClipboard);
            };

            timer.Start();
        }
        /// <summary>
        /// Вставляет текст из указанного буфера
        /// </summary>
        /// <param name="key">Нажатая клавиша, указывающая, из какого буфера будет вставлен текст</param>
        public void Paste(object sender, InputViewEventArgs key)
        {
            if (Storage[key.InputKey] != null && Storage[key.InputKey] != "")
            {
                InputSimulator inputSimulator = new InputSimulator();
                string contentsClipboard = Clipboard.GetText() ?? "";
                Clipboard.SetText(Storage[key.InputKey]);
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
                    Clipboard.SetText(contentsClipboard);
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
