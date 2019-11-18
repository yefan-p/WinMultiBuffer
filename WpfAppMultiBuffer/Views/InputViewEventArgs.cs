using System;
using System.Windows.Forms;

namespace WpfAppMultiBuffer.Views
{
    public class InputViewEventArgs : EventArgs
    {
        public readonly Keys InputKey;

        public InputViewEventArgs(Keys inputKey)
        {
            InputKey = inputKey;
        }
    }
}
