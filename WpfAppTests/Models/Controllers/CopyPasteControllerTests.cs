using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiBuffer.WpfApp.Models.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiBuffer.WpfAppTests.Models.Controllers.CopyPasteControllerMock;
using System.Diagnostics;
using System.Threading;

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
            var clipboardController = new ClipboardController(expectedValue);

            var answer = new List<string>();
            var trueAnswer = new List<string>(new [] { defaultBufferValue, expectedValue });

            var controller = new CopyPasteController<CopyPasteCollection>(
                new CopyPasteControllerMock.InputController(),
                new CopyPasteCollection(indexOfResult, defaultBufferValue),
                new BufferItemFactory(),
                clipboardController);

            clipboardController.IsSetText += (buffeerText) =>
            {
                answer.Add(buffeerText);
            };

            controller.Paste(
                null,
                new InputControllerEventArgs(System.Windows.Forms.Keys.None, ""));

            CollectionAssert.AreEqual(answer, trueAnswer);
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
            var inputController = new CopyPasteControllerMock.InputController();

            var controller = new CopyPasteController<CopyPasteCollection>(
                inputController,
                new CopyPasteCollection(indexOfResult, stringForBuffer),
                new BufferItemFactory(),
                new ClipboardController(expectedValue));

            inputController.OnPasteKeyPress();
        }

        [TestMethod()]
        public void CopyTest()
        {
            Assert.Fail();
        }
    }
}