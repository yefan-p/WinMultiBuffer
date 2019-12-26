using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiBuffer.WpfApp.Models.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiBuffer.WpfAppTests.Models.Controllers.CopyPasteControllerTestsMock;

namespace MultiBuffer.WpfAppTests.Models.Controllers.Tests
{
    [TestClass()]
    public class CopyPasteControllerTests
    {
        [TestMethod()]
        public void PasteTest()
        {
            var controller = new CopyPasteController<CopyPasteCollection>(
                new CopyPasteControllerTestsMock.InputController(), 
                new CopyPasteCollection(),
                new BufferItemFactory(),
                new InputSimulatorFactory(),
                new ClipboardControllerFactory()
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