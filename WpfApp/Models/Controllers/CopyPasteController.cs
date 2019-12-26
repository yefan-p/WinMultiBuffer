using System;
using System.Collections.Generic;
using System.Windows.Threading;
using WindowsInput;
using WindowsInput.Native;
using MultiBuffer.WpfApp.Models.Interfaces;

namespace MultiBuffer.WpfApp.Models.Controllers
{
    public class CopyPasteController<TCollection>
        : ICopyPasteController<TCollection>
        where TCollection : IList<IBufferItem>
    {
        public CopyPasteController(
                    IInputController inputController,
                    TCollection collection,
                    IBufferItemFactory bufferItemFactory,
                    IInputSimulatorFactory inputSimulatorFactory,
                    IClipboardControllerFactory clipboardControllerFactory)
        {
            Buffer = collection;
            _inputController = inputController;
            _bufferItemFactory = bufferItemFactory;
            _inputSimulatorFactory = inputSimulatorFactory;
            _clipboardControllerFactory = clipboardControllerFactory;

            _inputController.PasteKeyPress += Paste;
            _inputController.CopyKeyPress += Copy;
        }

        /// <summary>
        /// Событие возникает при встваке или удаления элемента в коллекцию
        /// </summary>
        public event Action<IBufferItem> Update;

        /// <summary>
        /// Предоставляет доступ к буферу обмена
        /// </summary>
        private readonly IClipboardControllerFactory _clipboardControllerFactory;

        /// <summary>
        /// Сообщает о событии вставки или копирования
        /// </summary>
        private readonly IInputController _inputController;

        /// <summary>
        /// Предоставляет экземпляр класса BufferItem
        /// </summary>
        private readonly IBufferItemFactory _bufferItemFactory;

        /// <summary>
        /// Предоставляет доступ к эмулятору нажатия клавиш, необходимо для копирования и вставки
        /// </summary>
        private readonly IInputSimulatorFactory _inputSimulatorFactory;

        /// <summary>
        /// Коллекция буферов
        /// </summary>
        public TCollection Buffer { get; private set; }

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
            IClipboardController clipboard = _clipboardControllerFactory.GetClipboard();
            string contentsClipboard = clipboard.GetText() ?? "";

            IBufferItem tmpItem = _bufferItemFactory.GetBuffer();
            tmpItem.CopyKey = key.CopyKey;
            tmpItem.PasteKey = key.PasteKey;

            int index = Buffer.IndexOf(tmpItem);
            if (index > -1)
            {
                clipboard.SetText(Buffer[index].Value);

                IInputSimulator simulator = _inputSimulatorFactory.GetInputSimulator();
                simulator.Keyboard.KeyDown(VirtualKeyCode.LCONTROL);
                simulator.Keyboard.KeyPress(VirtualKeyCode.VK_V);
                simulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);

                DispatcherTimer timer = new DispatcherTimer()
                {
                    Interval = TimeSpan.FromMilliseconds(Interval),
                };

                timer.Tick += (timerInner, eventArgs) =>
                {
                    timer.Stop();
                    clipboard.SetText(contentsClipboard);
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
            IClipboardController clipboard = _clipboardControllerFactory.GetClipboard();
            string contentsClipboard = clipboard.GetText() ?? "";

            IInputSimulator simulator = _inputSimulatorFactory.GetInputSimulator();
            simulator.Keyboard.KeyDown(VirtualKeyCode.LCONTROL);
            simulator.Keyboard.KeyPress(VirtualKeyCode.VK_C);
            simulator.Keyboard.KeyUp(VirtualKeyCode.LCONTROL);

            DispatcherTimer timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(Interval),
            };

            timer.Tick += (timerInner, eventArgs) =>
            {
                timer.Stop();

                IBufferItem tmpItem = _bufferItemFactory.GetBuffer();
                tmpItem.CopyKey = key.CopyKey;
                tmpItem.PasteKey = key.PasteKey;
                tmpItem.Value = clipboard.GetText();

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

                clipboard.SetText(contentsClipboard);

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
            Buffer.Remove(obj);
            Update?.Invoke(obj);
        }
    }
}
