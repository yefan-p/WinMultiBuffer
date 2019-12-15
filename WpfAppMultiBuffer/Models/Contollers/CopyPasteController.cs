using System;
using System.Collections.Generic;
using System.Windows.Threading;
using WindowsInput;
using WindowsInput.Native;
using WpfAppMultiBuffer.Models.Interfaces;

namespace WpfAppMultiBuffer.Models.Controllers
{
    public class CopyPasteController<TCollection, TItem>
        : ICopyPasteController<TCollection, TItem>
        where TCollection : IList<TItem>
        where TItem : IBufferItem
    {

        /// <summary>
        /// Событие возникает при встваке элемента в коллекцию
        /// </summary>
        public event Action<TItem> Update;

        /// <summary>
        /// Сообщает о событии вставки или копирования
        /// </summary>
        private readonly IInputController _inputController;

        private readonly IBufferItemFactory<TItem> _bufferItemFactory;

        /// <summary>
        /// Коллекция буферов
        /// </summary>
        public TCollection Buffer { get; private set; }

        public CopyPasteController(
            IInputController inputController,
            TCollection collection,
            IBufferItemFactory<TItem> bufferItemFactory)
        {
            Buffer = collection;
            _inputController = inputController;
            _bufferItemFactory = bufferItemFactory;

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

            TItem tmpItem = (TItem)_bufferItemFactory.GetBuffer();
            tmpItem.CopyKey = key.CopyKey;
            tmpItem.PasteKey = key.PasteKey;

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

                TItem tmpItem = (TItem)_bufferItemFactory.GetBuffer();
                tmpItem.CopyKey = key.CopyKey;
                tmpItem.PasteKey = key.PasteKey;
                tmpItem.Value = TextCopy.Clipboard.GetText();

                int index = Buffer.IndexOf(tmpItem);
                if (index > -1)
                {
                    Buffer[index].Value = tmpItem.Value;
                }
                else
                {
                    Buffer.Add(tmpItem);
                    tmpItem.Delete += TmpItem_Delete;
                }

                TextCopy.Clipboard.SetText(contentsClipboard);

                Update?.Invoke(tmpItem);
            };
            timer.Start();
        }

        private void TmpItem_Delete(IBufferItem obj)
        {
            TItem item = (TItem)obj;
            Buffer.Remove(item);
            Update?.Invoke(item);
        }
    }
}
