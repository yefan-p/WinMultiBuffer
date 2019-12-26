using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiBuffer.WpfApp.Models.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiBuffer.WpfAppTests.Models.Controllers.CopyPasteControllerMock;
using System.Diagnostics;

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
             * expectedValue - строка, которую вставляем
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

            var flag = false;
            clipboardFactory.ClipboardController.TextWasSet += (actual) =>
            {
                if (!flag)
                {
                    Assert.AreEqual(expectedValue, actual);
                    flag = true;
                }
                else
                {   // Сюда мы не хочет заходить. А надо.
                    Assert.AreEqual(defaultBufferValue, actual);
                    flag = false;
                }
            };
            inputController.OnPasteKeyPress();
        }

        [TestMethod()]
        public void PasteTestBufferNotExist()
        {
            /* Буфер не существет - indexOfResult <= -1; 
             * Буфер существует - indexOfResult > -1
             * stringForBuffer - строка, которую вставляем
             * expectedValue - значение, которое устанавливается в буфере по умолчанию
             */
            var indexOfResult = -1;
            var stringForBuffer = "Buffer not exist case";
            var expectedValue = "String for buffer";
            var clipboardFactory = new ClipboardControllerFactory(expectedValue);
            var inputController = new CopyPasteControllerMock.InputController();

            var controller = new CopyPasteController<CopyPasteCollection>(
                inputController,
                new CopyPasteCollection(indexOfResult, stringForBuffer),
                new BufferItemFactory(),
                new InputSimulatorFactory(),
                clipboardFactory
                );
            inputController.OnPasteKeyPress();

            string actual = clipboardFactory.ClipboardController.GetText();
            Assert.AreEqual(expectedValue, actual);
        }

        [TestMethod()]
        public void CopyTest()
        {
            Assert.Fail();
        }
    }
}