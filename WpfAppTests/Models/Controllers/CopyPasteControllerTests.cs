using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfAppMultiBuffer.Models.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppMultiBufferTests.Mock;

namespace WpfAppMultiBuffer.Models.Controllers.Tests
{
    [TestClass()]
    public class CopyPasteControllerTests
    {
        [TestMethod()]
        public void PasteTest()
        {
            var controller = new CopyPasteController<CopyPasteCollectionMock>(
                new InputControllerMock(), 
                new CopyPasteCollectionMock(),
                new BufferItemFactoryMock(),
                new InputSimulatorFactoryMock(),
                new ClipboardControllerFactoryMock()
                );
            Assert.Fail();
        }

        [TestMethod()]
        public void CopyTest()
        {
            Assert.Fail();
        }
    }
}