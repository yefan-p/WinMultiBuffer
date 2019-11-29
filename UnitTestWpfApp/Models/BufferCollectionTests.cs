using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfAppMultiBuffer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public void AddRangeTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ClearTest()
        {
            Assert.Fail();
        }
    }
}