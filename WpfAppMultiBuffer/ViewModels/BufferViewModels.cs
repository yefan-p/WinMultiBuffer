using System.Linq;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;
using TextCopy;
using WpfAppMultiBuffer.Models;
using WpfAppMultiBuffer.Views;

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
        public void KeyPress(object sender, InputViewEventArgs key)
        {
            InputSimulator inputSimulator = new InputSimulator();

            if (Literals.KeysCopy.Contains(key.InputKey))
            {
                string contentsClipboard = TextCopy.Clipboard.GetText() ?? "";
                inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LCONTROL);
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_C);
                inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);

                Timer timer = new Timer();//TODO: Сделать нормальный асинхорн, убрать таймер
                timer.Tick += (timerInner, eventArgs) =>
                {
                    timer.Stop();
                    Storage[key.InputKey] = TextCopy.Clipboard.GetText();
                    TextCopy.Clipboard.SetText(contentsClipboard);
                    timer.Dispose();
                };
                timer.Interval = Literals.Interval;
                timer.Start();
            }
            else if (Literals.KeysPaste.Contains(key.InputKey) && Storage[key.InputKey] != null && Storage[key.InputKey] != "")
            {
                string contentsClipboard = TextCopy.Clipboard.GetText() ?? "";
                TextCopy.Clipboard.SetText(Storage[key.InputKey]);
                inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LCONTROL);
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_V);
                inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);

                Timer timer = new Timer();//TODO: Сделать нормальный асинхорн, убрать таймер
                timer.Tick += (timerInner, eventArgs) =>
                {
                    timer.Stop();
                    TextCopy.Clipboard.SetText(contentsClipboard);
                    timer.Dispose();
                };
                timer.Interval = Literals.Interval;
                timer.Start();
            }
        }
    }
}
