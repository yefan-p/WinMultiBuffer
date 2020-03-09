using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MultiBuffer.WebApi.DataModel;

namespace MultiBuffer.WebApi.Controllers
{
    [RoutePrefix("api/Buffers")]
    public class BuffersController : ApiController
    {

        // GET: api/Buffers/5
        [HttpGet, Route("{intKey}")]
        [ResponseType(typeof(BufferItem))]
        public BufferItem Read(int intKey)
        {
            var context = new MultiBufferContext();

            var query =
                from el in context.BufferItems
                where el.Key == intKey
                select el;

            return query.SingleOrDefault();
        }

        // DELETE: api/Buffers/5
        [HttpDelete, Route("{intKey}")]
        [ResponseType(typeof(BufferItem))]
        public IHttpActionResult Delete(int intKey)
        {
            var context = new MultiBufferContext();

            var query =
                from el in context.BufferItems
                where el.Key == intKey
                select el;

            BufferItem item = query.SingleOrDefault();
            if (item != null)
            {
                try
                {
                    context.BufferItems.Remove(item);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return BadRequest("Item is already not existing");
            }
        }

        // PUT: api/Buffers/5
        [HttpPut, Route("{intKey}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult Update(int intKey, BufferItem bufferItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (intKey != bufferItem.Key)
            {
                return BadRequest("Wrong data");
            }

            var context = new MultiBufferContext();
            var query =
                from el in context.BufferItems
                where el.Key == intKey
                select el;

            BufferItem item = query.SingleOrDefault();
            if (item == null)
            {
                return BadRequest("Item is not existing");
            }

            item.Value = bufferItem.Value;

            try
            {
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Buffers/5
        [HttpPost, Route("{intKey}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult Create(int intKey, BufferItem bufferItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (intKey != bufferItem.Key)
            {
                return BadRequest("Wrong data");
            }

            var context = new MultiBufferContext();
            var query =
                from el in context.BufferItems
                where el.Key == intKey
                select el;

            BufferItem item = query.SingleOrDefault();
            if (item == null)
            {
                try
                {
                    bufferItem.Id = 0;
                    context.BufferItems.Add(bufferItem);
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                return StatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                return BadRequest("Item is already existing");
            }
        }
    }
}
