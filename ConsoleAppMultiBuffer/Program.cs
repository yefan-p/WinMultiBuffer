using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gma.System.MouseKeyHook;

namespace ConsoleAppMultiBuffer
{
    class Program
    {
        static IKeyboardEvents keyboardEvents;

        static void Main(string[] args)
        {
            keyboardEvents = Hook.GlobalEvents();
            keyboardEvents.KeyPress += KeyboardEvents_KeyPress;
        }

        private void KeyboardEvents_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {

        }
    }
}
