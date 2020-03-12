using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiBuffer.WpfApp.Models.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiBuffer.WpfApp.ViewModels.Implements;
using MultiBuffer.WpfApp.Models.Interfaces;
using MultiBuffer.WpfApp.Models.DataModels;
using System.Diagnostics;

namespace MultiBuffer.WpfApp.Models.Handlers.Tests
{
    [TestClass()]
    public class StorageWebApiHandlerTests
    {
        [TestMethod()]
        public async Task CreateAsyncTest()
        {
            var item = new BufferItem
            {
                Key = System.Windows.Forms.Keys.Shift,
                Value = "Create WpfApp client test"
            };

            var webApi = new StorageWebApiHandler();
            await webApi.CreateAsync(item);
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public async Task ReadAsyncTest()
        {
            var webApi = new StorageWebApiHandler();
            BufferItemDataModel item = await webApi.ReadAsync(32);

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

            var webApi = new StorageWebApiHandler();
            await webApi.UpdateAsync(item);
            Assert.IsTrue(true);
        }
    }
}