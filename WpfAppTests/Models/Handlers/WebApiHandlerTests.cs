using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiBuffer.WpfApp.Models.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiBuffer.WpfApp.ViewModels.Implements;
using MultiBuffer.IWebApi;
using System.Diagnostics;

namespace MultiBuffer.WpfApp.Models.Handlers.Tests
{
    [TestClass()]
    public class WebApiHandlerTests
    {
        [TestMethod()]
        public async Task CreateAsyncTest()
        {
            var item = new BufferItem
            {
                Key = System.Windows.Forms.Keys.Shift,
                Value = "Create WpfApp client test"
            };

            var webApi = new WebApiHandler();
            await webApi.AuthUser("admin", "admin");
            await webApi.CreateAsync(item);
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public async Task ReadAsyncTest()
        {
            var webApi = new WebApiHandler();
            WebBuffer item = await webApi.ReadAsync(4);

            Debug.WriteLine(item.Name);
            Debug.WriteLine(item.Key);
            Debug.WriteLine(item.Value);
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public async Task UpdateAsyncTest()
        {
            var item = new BufferItem
            {
                Key = System.Windows.Forms.Keys.Shift,
                Value = "Update WpfApp client test"
            };

            var webApi = new WebApiHandler();
            await webApi.UpdateAsync(item);
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public async Task DeleteAsyncTest()
        {
            var webApi = new WebApiHandler();
            await webApi.DeleteAsync(5);

            Assert.IsTrue(true);
        }

        [TestMethod()]
        public async Task AuthUserTest()
        {
            var webApi = new WebApiHandler();
            await webApi.AuthUser("admin", "admin");

            Assert.IsTrue(true);
        }

        [TestMethod()]
        public async Task ReadListAsyncTest()
        {
            var webApi = new WebApiHandler();
            await webApi.AuthUser("admin", "admin");
            IEnumerable<WebBuffer> buffers = await webApi.ReadListAsync();

            Assert.IsTrue(true);
        }
    }
}