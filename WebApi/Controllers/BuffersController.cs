using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MultiBuffer.WebApi.DataModel;

namespace MultiBuffer.WebApi.Controllers
{
    [RoutePrefix("api/Buffers")]
    public class BuffersController : ApiController
    {

        // GET: api/Buffers/5
        [HttpGet, Route("{intKey}")]
        public BufferItem Get(int intKey)
        {
            var context = new MultiBufferContext();

            var query =
                from el in context.BufferItems
                where el.Key == intKey
                select el;

            return query.SingleOrDefault();
        }
    }
}
