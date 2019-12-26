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
        public void PasteTestBufferExist()
        {
            /* Буфер не существет - indexOfResult <= -1; 
             * Буфер существует - indexOfResult > -1
             * expectedValue - значение, которое должно вставиться из буфера, если буфер существует
             * defaultBufferValue - значение, которое устанавливается в буфере по умолчанию
             */
            var indexOfResult = 0;
            var expectedValue = "Buffer exist case";
            var defaultBufferValue = "String for buffer";
            var clipboardFactory = new ClipboardControllerFactory(defaultBufferValue);
            var inputController = new CopyPasteControllerMock.InputController();

            var controller = new CopyPasteController<CopyPasteCollection>(
                inputController,
                new CopyPasteCollection(indexOfResult, expectedValue),
                new BufferItemFactory(),
                new InputSimulatorFactory(),
                clipboardFactory
                );

            controller.Update += (e) =>
            {
                string actual = clipboardFactory.ClipboardController.GetText();
                Assert.AreEqual(expectedValue, actual);
            };

            inputController.OnPasteKeyPress();
        }

        [TestMethod()]
        public void PasteTestBufferNotExist()
        {
            /* Буфер не существет - indexOfResult <= -1; 
             * Буфер существует - indexOfResult > -1
             * expectedValue - значение, которое должно вставиться из буфера, если буфер существует
             * defaultBufferValue - значение, которое устанавливается в буфере по умолчанию
             */
            var indexOfResult = -1;
            var expectedValue = "Buffer exist case";
            var defaultBufferValue = "String for buffer";
            var clipboardFactory = new ClipboardControllerFactory(defaultBufferValue);
            var inputController = new CopyPasteControllerMock.InputController();

            var controller = new CopyPasteController<CopyPasteCollection>(
                inputController,
                new CopyPasteCollection(indexOfResult, expectedValue),
                new BufferItemFactory(),
                new InputSimulatorFactory(),
                clipboardFactory
                );
            inputController.OnPasteKeyPress();

            string actual = clipboardFactory.ClipboardController.GetText();
            Assert.AreEqual(defaultBufferValue, actual);
        }

        [TestMethod()]
        public void CopyTest()
        {
            Assert.Fail();
        }
    }
}