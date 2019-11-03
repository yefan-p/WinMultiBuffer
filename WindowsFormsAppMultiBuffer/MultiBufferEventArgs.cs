using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsAppMultiBuffer
{
    class MultiBufferEventArgs : EventArgs
    {
        public readonly TwiceKeyDictionary<Keys, string> Storage;

        public MultiBufferEventArgs(TwiceKeyDictionary<Keys, string> storage)
        {
            Storage = storage;
        }
    }
}
