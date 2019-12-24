using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DAL.Models;
using WebApiMultiBuffer.Models;

namespace WebApiMultiBuffer.Controllers
{
    [RoutePrefix("api/Buffers")]
    public class BuffersController : ApiController
    {

        // GET: api/Buffers
        [HttpGet, Route("")]
        public IEnumerable<BuffersModel> Get()
        {
            MultiBufferContext context = new MultiBufferContext();
            IEnumerable<BuffersModel> result =
                (from el in context.tblClipboards
                 select new BuffersModel
                 {
                     Id = el.id,
                     User = el.idUser,
                     Name = el.nvcName,
                     Value = el.nvcValue,
                     CopyKeyCode = el.intCopyKeyCode,
                     PasteKeyCode = el.intPasteKeyCode,
                 }).ToList();

            return result;
        }
    }
}
