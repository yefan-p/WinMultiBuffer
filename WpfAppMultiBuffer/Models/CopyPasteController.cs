using System;
using System.Collections.Generic;
using System.Windows.Threading;
using WindowsInput;
using WindowsInput.Native;
using WpfAppMultiBuffer.Controllers;
using WpfAppMultiBuffer.Interfaces;

namespace WpfAppMultiBuffer.Models
{
    public class CopyPasteController : ICopyPasteController
    {
        public event Action<BufferItem> Update;

        private readonly IInputController _inputController;

        private readonly List<BufferItem> buffer;

        public CopyPasteController(IInputController inputController)
        {
            buffer = new List<BufferItem>();
            _inputController = inputController;

            _inputController.PasteKeyPress += Paste;
            _inputController.CopyKeyPress += Copy;
        }

        /// <summary>
        /// Количество миллисекунд, которые должны пройти, прежде чем произойдет обращение к буферу обмена после нажатия клавиши.
        /// Задержка необходима для того, чтобы выделенный текст успел дойти до буфера обмена при копировании или успел вставиться при вставке.
        /// </summary>
        const int Interval = 250;

        /// <summary>
        /// Вставляет текст из указанного буфера
        /// </summary>
        /// <param name="key">Нажатая клавиша, указывающая, из какого буфера будет вставлен текст</param>
        public void Paste(object sender, InputControllerEventArgs key)
        {
            InputSimulator inputSimulator = new InputSimulator();
            string contentsClipboard = TextCopy.Clipboard.GetText() ?? "";

            BufferItem tmpItem = new BufferItem()
            {
                CopyKey = key.CopyKey,
                PasteKey = key.PasteKey,
            };

            int index = buffer.IndexOf(tmpItem);
            if (index > -1)
            {
                TextCopy.Clipboard.SetText(buffer[index].Value);

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
        }

        /// <summary>
        /// Копирует текст в указанный буфер
        /// </summary>
        /// <param name="key">Нажатая клавиша, указывающая, в какой буфер будет вставлен текст</param>
        public void Copy(object sender, InputControllerEventArgs key)
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

                BufferItem tmpItem = new BufferItem()
                {
                    CopyKey = key.CopyKey,
                    PasteKey = key.PasteKey,
                    Value = TextCopy.Clipboard.GetText(),
                };

                int index = buffer.IndexOf(tmpItem);
                if (index > -1)
                {
                    buffer[index] = tmpItem;
                }
                else
                {
                    buffer.Add(tmpItem);
                }

                TextCopy.Clipboard.SetText(contentsClipboard);

                Update?.Invoke(tmpItem);
            };
            timer.Start();
        }
    }
}
