using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiBuffer.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using MultiBuffer.WebApi.Utils;
using Microsoft.Extensions.Options;
using WebApiTests.Mock;
using MultiBuffer.IWebApi;

namespace MultiBuffer.WebApi.Controllers.Tests
{
    [TestClass()]
    public class BuffersControllerTests
    {
        [TestMethod()]
        public void CreateListTest()
        {
            var controller = new BuffersController(new UserSeviceTest());
            var list = new List<WebBuffer>
            {
                new WebBuffer{Key = 0, Name = "None", Value = "NewBuffer1"},
                new WebBuffer{Key = 0, Name = "None", Value = "NewBuffer2"},
                new WebBuffer{Key = 0, Name = "None", Value = "NewBuffer3"},
                new WebBuffer{Key = 49, Name = "D1", Value = "UpdateBuffer1"},
                new WebBuffer{Key = 50, Name = "D2", Value = "UpdateBuffer2"}
            };
            controller.CreateList(list);
            Assert.IsTrue(true);
        }
    }
}