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
        /// Событие возникает при встваке или удаления элемента в коллекцию
        /// </summary>
        public event Action<TItem> Update;

        /// <summary>
        /// Сообщает о событии вставки или копирования
        /// </summary>
        private readonly IInputController _inputController;

        /// <summary>
        /// Предоставляет экземпляр класса BufferItem
        /// </summary>
        private readonly IBufferItemFactory<TItem> _bufferItemFactory;

        /// <summary>
        /// Эмулирует нажатия на клавиши, необходимо для копирования и вставки
        /// </summary>
        private readonly IInputSimulator _inputSimulator;

        /// <summary>
        /// Коллекция буферов
        /// </summary>
        public TCollection Buffer { get; private set; }

        public CopyPasteController(
            IInputController inputController,
            TCollection collection,
            IBufferItemFactory<TItem> bufferItemFactory,
            IInputSimulatorFactory inputSimulatorFactory)
        {
            Buffer = collection;
            _inputController = inputController;
            _bufferItemFactory = bufferItemFactory;
            _inputSimulator = inputSimulatorFactory.GetInputSimulator();

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
            string contentsClipboard = TextCopy.Clipboard.GetText() ?? "";

            TItem tmpItem = (TItem)_bufferItemFactory.GetBuffer();
            tmpItem.CopyKey = key.CopyKey;
            tmpItem.PasteKey = key.PasteKey;

            int index = Buffer.IndexOf(tmpItem);
            if (index > -1)
            {
                TextCopy.Clipboard.SetText(Buffer[index].Value);

                _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LCONTROL);
                _inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_V);
                _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);

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
            string contentsClipboard = TextCopy.Clipboard.GetText() ?? "";
            _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.LCONTROL);
            _inputSimulator.Keyboard.KeyPress(VirtualKeyCode.VK_C);
            _inputSimulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);

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

        /// <summary>
        /// Удаляет выбранный буфер
        /// </summary>
        /// <param name="obj"></param>
        private void TmpItem_Delete(IBufferItem obj)
        {
            TItem item = (TItem)obj;
            Buffer.Remove(item);
            Update?.Invoke(item);
        }
    }
}
