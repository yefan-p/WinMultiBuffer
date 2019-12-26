using System;
using WpfAppMultiBuffer.Models.Controllers;
using WpfAppMultiBuffer.Models.Interfaces;
using System.Windows.Forms;

namespace MultiBuffer.WpfAppTests.Models.Controllers.CopyPasteControllerTestsMock
{
    public class InputController : IInputController
    {
        public event EventHandler<InputControllerEventArgs> PasteKeyPress;
        public event EventHandler<InputControllerEventArgs> CopyKeyPress;

        /// <summary>
        /// Вызывает событие вставки
        /// </summary>
        public void OnPasteKeyPress()
        {
            PasteKeyPress?.Invoke(this, new InputControllerEventArgs(Keys.None, Keys.None));
        }

        /// <summary>
        /// Вызывает событие копирования
        /// </summary>
        public void OnCopyKeyPress()
        {
            CopyKeyPress?.Invoke(this, new InputControllerEventArgs(Keys.None, Keys.None));
        }
    }
}
