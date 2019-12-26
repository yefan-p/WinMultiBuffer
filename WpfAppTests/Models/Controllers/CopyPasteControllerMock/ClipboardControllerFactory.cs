using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppMultiBuffer.Models.Interfaces;

namespace MultiBuffer.WpfAppTests.Models.Controllers.CopyPasteControllerTestsMock
{
    public class ClipboardControllerFactory : IClipboardControllerFactory
    {
        public IClipboardController GetClipboard()
        {
            throw new NotImplementedException();
        }
    }
}
