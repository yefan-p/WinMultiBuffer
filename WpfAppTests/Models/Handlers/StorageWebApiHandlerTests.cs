using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiBuffer.WpfApp.Models.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiBuffer.WpfApp.ViewModels.Implements;

namespace MultiBuffer.WpfApp.Models.Handlers.Tests
{
    [TestClass()]
    public class StorageWebApiHandlerTests
    {
        [TestMethod()]
        public void CreateAsyncTest()
        {
            var item = new BufferItem
            {
                Key = System.Windows.Forms.Keys.Control,
                Value = "Create WpfApp client test"
            };

            var webApi = new StorageWebApiHandler();
            webApi.CreateAsync(item);

            while(true)
            Assert.IsTrue(true);
        }
    }
}