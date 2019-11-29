using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfAppMultiBuffer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WpfAppMultiBuffer.Views;

namespace WpfAppMultiBuffer.Models.Tests
{
    [TestClass()]
    public class BufferCollectionTests
    {
        [TestMethod()]
        public void AddTest()
        {
            BufferCollection bufferItems = new BufferCollection();
            bufferItems.Add(Keys.D1, Keys.Q, "Test 1/Q");
            Assert.AreEqual("Test 1/Q", bufferItems[Keys.D1]);
        }

        [TestMethod()]
        public void AddTestFor()
        {
            BufferCollection bufferItems = new BufferCollection();

            for (int i = 0; i < InputView.KeysCopy.Length; i++)
            {
                bufferItems.Add(InputView.KeysCopy[i], InputView.KeysPaste[i], i.ToString());
                Assert.AreEqual(i.ToString(), bufferItems[InputView.KeysCopy[i]]);
                Assert.AreEqual(i.ToString(), bufferItems[InputView.KeysPaste[i]]);
            }
        }

        [TestMethod()]
        public void AddRangeTest()
        {
            BufferCollection bufferItems = new BufferCollection();
            bufferItems.AddRange(InputView.KeysCopy, InputView.KeysPaste, "Range Test");

            foreach (var item in InputView.KeysCopy)
            {
                Assert.AreEqual("Range Test", bufferItems[item]);
            }

            foreach (var item in InputView.KeysPaste)
            {
                Assert.AreEqual("Range Test", bufferItems[item]);
            }
        }

        [TestMethod()]
        public void ClearTest()
        {
            BufferCollection bufferItems = new BufferCollection();
            bufferItems.Add(Keys.D1, Keys.Q, "Test 1/Q");
            bufferItems.Clear(0);
            Assert.AreEqual("", bufferItems[Keys.D1]);
        }
    }
}