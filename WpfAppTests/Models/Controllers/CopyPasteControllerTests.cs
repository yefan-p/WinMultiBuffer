using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiBuffer.WpfApp.Models.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiBuffer.WpfAppTests.Models.Controllers.CopyPasteControllerMock;

namespace MultiBuffer.WpfAppTests.Models.Controllers.Tests
{
    [TestClass()]
    public class CopyPasteControllerTests
    {
        [TestMethod()]
        public void PasteTest()
        {
            /* indexOfResult <= -1; indexOfResult > -1
             * expectedValueCase1 - значение, которое должен получить буфер, если значение indexOfResult > -1
             * expectedValueCase2 - значение, которое должно вернусться из буфера, если значение indexOfResult <= -1
             */
            var inputController = new CopyPasteControllerMock.InputController();
            var indexOfResult = 0;
            var expectedValueCase1 = "Case 1 was happend";
            var expectedValueCase2 = "Case 2 was happend";
            var clipboardFactory = new ClipboardControllerFactory(expectedValueCase2);
            var controller = new CopyPasteController<CopyPasteCollection>(
                inputController,
                new CopyPasteCollection(indexOfResult, expectedValueCase1),
                new BufferItemFactory(),
                new InputSimulatorFactory(),
                clipboardFactory
                );

            controller.Update += (e) =>
            {
                string actual = clipboardFactory.ClipboardController.GetText();
                Assert.AreEqual(expectedValueCase1, actual);
            };

            inputController.OnPasteKeyPress();
        }

        [TestMethod()]
        public void CopyTest()
        {
            Assert.Fail();
        }
    }
}