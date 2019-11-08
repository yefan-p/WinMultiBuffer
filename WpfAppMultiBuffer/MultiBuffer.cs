using System;
using System.Linq;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;
using TextCopy;

namespace WpfAppMultiBuffer
{
    class MultiBuffer
    {
        /// <summary>
        /// Флаг, который указывает, ожидает ли программа ожидает нажатие клавиши
        /// </summary>
        public bool IsActive { get; set; } = false;
        /// <summary>
        /// Вызывается при добавлении или обновлении содержимого в хранилище
        /// </summary>
        public event EventHandler<EventArgs> BufferUpdate;
        /// <summary>
        /// Хранилище скопированного текста
        /// </summary>
        readonly TwiceKeyDictionary<Keys, string> _storage = new TwiceKeyDictionary<Keys, string>();

        public MultiBuffer()
        {
            _storage.AddRange(MultiBufferLiterals.KeysCopy, MultiBufferLiterals.KeysPaste, "");
        }

        /// <summary>
        /// Обработчик нажатия на клавишу
        /// </summary>
        /// <param name="key"></param>
        public void KeyDownManager(Keys key)
        {
            InputSimulator inputSimulator = new InputSimulator();
            Timer timer = new Timer();

            if (MultiBufferLiterals.KeysCopy.Contains(key))
            {
                string contentsClipboard = TextCopy.Clipboard.GetText() ?? "";
                inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LCONTROL);
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_C);
                inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);

                timer.Tick += (timerInner, eventArgs) =>
                {
                    timer.Stop();
                    _storage[key] = TextCopy.Clipboard.GetText();
                    BufferUpdate?.Invoke(this, EventArgs.Empty);
                    TextCopy.Clipboard.SetText(contentsClipboard);
                };
                timer.Interval = MultiBufferLiterals.Interval;
                timer.Start();
            }
            else if (MultiBufferLiterals.KeysPaste.Contains(key) && _storage[key] != null && _storage[key] != "")
            {
                string contentsClipboard = TextCopy.Clipboard.GetText() ?? "";
                TextCopy.Clipboard.SetText(_storage[key]);
                inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LCONTROL);
                inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_V);
                inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);

                timer.Tick += (timerInner, eventArgs) =>
                {
                    timer.Stop();
                    TextCopy.Clipboard.SetText(contentsClipboard);
                };
                timer.Interval = MultiBufferLiterals.Interval;
                timer.Start();
            }
            timer.Dispose();
        }
    }
}
