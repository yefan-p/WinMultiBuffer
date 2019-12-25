using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppMultiBuffer.Models.Interfaces;

namespace WpfAppMultiBufferTests.Mock
{
    public class ClipboardControllerFactoryMock : IClipboardControllerFactory
    {
        public IClipboardController GetClipboard()
        {
            throw new NotImplementedException();
        }
    }
}
