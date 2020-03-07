using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiBuffer.WebApi.Controllers;
using MultiBuffer.WebApi.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiBuffer.WebApi.Controllers.Tests
{
    [TestClass()]
    public class BuffersControllerTests
    {
        [TestMethod()]
        public void GetTest()
        {
            var buffersController = new BuffersController();
            BufferItem bufferItemActual = buffersController.Get(0);
            var bufferItemExpected = new BufferItem
            {
                Id = 1,
                Name = "Zero",
                Key = 0,
                Value = "000"
            };
            Assert.Equals(bufferItemExpected, bufferItemActual);
        }
    }
}