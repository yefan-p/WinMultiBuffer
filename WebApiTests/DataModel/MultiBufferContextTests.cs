using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultiBuffer.WebApi.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiBuffer.WebApi.DataModel.Tests
{
    [TestClass()]
    public class MultiBufferContextTests
    {
        [TestMethod()]
        public void MultiBufferContextTest()
        {
            using (var context = new MultiBufferContext())
            {
                var bufferItem = new BufferItem()
                {
                    Key = 0,
                    Name = "Zero",
                    Value = "000"
                };
                context.BufferItems.Add(bufferItem);
                context.SaveChanges();
            }
            Assert.IsTrue(true);
        }
    }
}