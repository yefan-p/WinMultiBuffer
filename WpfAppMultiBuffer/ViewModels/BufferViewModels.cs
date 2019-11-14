using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;
using TextCopy;
using WpfAppMultiBuffer.Models;

namespace WpfAppMultiBuffer.ViewModels
{
    class BufferViewModels
    {
        public BufferViewModels()
        {
            Storage.AddRange(Literals.KeysCopy, Literals.KeysPaste, "");
        }
        /// <summary>
        /// Флаг, который указывает, ожидает ли программа ожидает нажатие клавиши
        /// </summary>
        public bool IsActive { get; set; } = false;
        /// <summary>
        /// Содержимое буфера
        /// </summary>
        public BufferCollection Storage { get; set; } = new BufferCollection();
        /// <summary>
        /// Вставить или копировать текст, зависит от нажатой клавиши
        /// </summary>
        /// <param name="key">Нажатая клавиша</param>
        public void CopyPaste(Keys key)
        {
            InputSimulator inputSimulator = new InputSimulator();

            if (Literals.KeysCopy.Contains(key))
            {
                string contentsClipboard = TextCopy.Clipboard.GetText() ?? "";
                inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LCONTROL);
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_C);
                inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);

                Timer timer = new Timer();//TODO: Сделать нормальный асинхорн, убрать таймер
                timer.Tick += (timerInner, eventArgs) =>
                {
                    timer.Stop();
                    Storage[key] = TextCopy.Clipboard.GetText();
                    TextCopy.Clipboard.SetText(contentsClipboard);
                    timer.Dispose();
                };
                timer.Interval = Literals.Interval;
                timer.Start();
            }
            else if (Literals.KeysPaste.Contains(key) && Storage[key] != null && Storage[key] != "")
            {
                string contentsClipboard = TextCopy.Clipboard.GetText() ?? "";
                TextCopy.Clipboard.SetText(Storage[key]);
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
