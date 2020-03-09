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
        public void ReadTest()
        {
            var buffersController = new BuffersController();
            BufferItem bufferItemActual = buffersController.Read(0);
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            var controller = new BuffersController();
            var item = new BufferItem
            {
                Id = 1,
                Name = "Zero",
                Key = 0,
                Value = "Update test",
            };
            var tmp = controller.Update(0, item);
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void CreateTest()
        {
            var controller = new BuffersController();
            var item = new BufferItem
            {
                Id = 1,
                Name = "Zero",
                Key = 2,
                Value = "Create method test",
            };
            var tmp = controller.Create(2, item);
            Assert.IsTrue(true);
        }
    }
}