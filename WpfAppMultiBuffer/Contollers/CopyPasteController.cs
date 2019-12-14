using System;
using System.Collections.Generic;
using System.Windows.Threading;
using WindowsInput;
using WindowsInput.Native;
using WpfAppMultiBuffer.Interfaces;
using WpfAppMultiBuffer.Models;

namespace WpfAppMultiBuffer.Controllers
{
    public class CopyPasteController<TCollection>
        : ICopyPasteController<TCollection> where TCollection : IList<BufferItem>
    {

        /// <summary>
        /// Событие возникает при встваке элемента в коллекцию
        /// </summary>
        public event Action<BufferItem> Update;

        /// <summary>
        /// Сообщает о событии вставки или копирования
        /// </summary>
        private readonly IInputController _inputController;

        /// <summary>
        /// Коллекция буферов
        /// </summary>
        public TCollection Buffer { get; private set; }

        public CopyPasteController(IInputController inputController, TCollection collection)
        {
            Buffer = collection;
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

            int index = Buffer.IndexOf(tmpItem);
            if (index > -1)
            {
                TextCopy.Clipboard.SetText(Buffer[index].Value);

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

                int index = Buffer.IndexOf(tmpItem);
                if (index > -1)
                {
                    Buffer[index] = tmpItem;
                }
                else
                {
                    Buffer.Add(tmpItem);
                }

                TextCopy.Clipboard.SetText(contentsClipboard);

                Update?.Invoke(tmpItem);
            };
            timer.Start();
        }
    }
}
