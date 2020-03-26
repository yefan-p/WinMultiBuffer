using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiBuffer.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using MultiBuffer.WebApi.Utils;
using Microsoft.Extensions.Options;
using WebApiTests.Mock;
using MultiBuffer.IWebApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Controllers;

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
                new WebBuffer{Key = 1, Name = "One", Value = "NewBuffer1"},
                new WebBuffer{Key = 2, Name = "Two", Value = "NewBuffer2"},
                new WebBuffer{Key = 3, Name = "Three", Value = "NewBuffer3"},
                new WebBuffer{Key = 49, Name = "D1", Value = "UpdateBuffer1"},
                new WebBuffer{Key = 50, Name = "D2", Value = "UpdateBuffer2"}
            };
            var actionContext = new ActionContext(new HttpContextTest(), 
                                                  new RouteData(),
                                                  new ControllerActionDescriptor());
            var ctrlContext = new ControllerContext(actionContext);
            controller.ControllerContext = ctrlContext;
            controller.RefreshList(list);
            Assert.IsTrue(true);
        }
    }
}