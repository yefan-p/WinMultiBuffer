using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DAL.Models;

namespace WebApiMultiBuffer.Controllers
{
    [RoutePrefix("api/Buffers")]
    public class BuffersController : ApiController
    {

        // GET: api/Buffers
        [HttpGet, Route("")]
        public IEnumerable<BuffersModel> Get()
        {
            
        }
    }
}
