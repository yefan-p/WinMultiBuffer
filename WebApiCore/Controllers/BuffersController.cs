using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiBuffer.WebApiCore.DataModels;
using MultiBuffer.WebApiInterfaces;
using MultiBuffer.WebApiCore.Utils;

namespace MultiBuffer.WebApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuffersController : ControllerBase
    {
        /// <summary>
        /// Добавляет экземплят буфера в базу.
        /// </summary>
        /// <param name="bufferItem">Экземпляр буфера</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(BufferItemWebApi bufferItem)
        {
            var contextDb = new MultiBufferContext();
            var query =
                from el in contextDb.BufferItems
                where el.Key == bufferItem.Key
                select el;

            BufferItem item = query.SingleOrDefault();
            if (item == null)
            {
                try
                {
                    item = new BufferItem()
                    {
                        Name = bufferItem.Name,
                        Value = bufferItem.Value,
                        Key = bufferItem.Key,
                    };

                    contextDb.BufferItems.Add(item);
                    contextDb.SaveChanges();
                }
                catch (Exception)
                {
                    return RequestResult.ServerError;
                }
                return RequestResult.Success;
            }
            return RequestResult.ClientError;
        }
    }
}