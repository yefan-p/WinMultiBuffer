using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MultiBuffer.WebApi.DataModels;
using MultiBuffer.IWebApi;
using MultiBuffer.WebApi.Utils;

namespace MultiBuffer.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BuffersController : ControllerBase
    {
        public BuffersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Добавляет экземплят буфера в базу.
        /// </summary>
        /// <param name="bufferItem">Экземпляр буфера</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(WebBuffer bufferItem)
        {
            User user = _userService.GetUserByClaims(HttpContext.User);
            if (user == null) return RequestResult.ClientError;

            var contextDb = new MultiBufferContext();
            var queryBuffer =
                from el in contextDb.BufferItems
                where el.Key == bufferItem.Key && el.UserId == user.Id
                select el;
            BufferItem item = queryBuffer.SingleOrDefault();

            if (item != null) return RequestResult.ClientError;

            try
            {
                item = new BufferItem()
                {
                    Name = bufferItem.Name,
                    Value = bufferItem.Value,
                    Key = bufferItem.Key,
                    UserId = user.Id,
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

        /// <summary>
        /// Добавляет коллекцию буферов в базу
        /// </summary>
        /// <param name="localBuffers">Список буферов, которые необходимо добавить</param>
        /// <returns></returns>
        [HttpPost("addlist")]
        public IActionResult CreateList(IEnumerable<WebBuffer> localBuffers)
        {
            User user = _userService.GetUserByClaims(HttpContext.User);
            if (user == null) return RequestResult.ClientError;

            var context = new MultiBufferContext();
            var queryOldBuffers =
                from el in context.BufferItems
                where el.UserId == user.Id
                select el;
            IEnumerable<BufferItem> oldBuffers = queryOldBuffers.ToList();

            var queryNewBuffers =
                from el in localBuffers
                select new BufferItem
                {
                    Key = el.Key,
                    Name = el.Name,
                    Value = el.Value,
                    UserId = user.Id
                };
            IEnumerable<BufferItem> creatBuffers = queryNewBuffers.Except(oldBuffers).ToList();
            IEnumerable<BufferItem> updateBuffres = oldBuffers.Except(creatBuffers).ToList();

            foreach (BufferItem item in updateBuffres)
            {
                int key = item.Key;
                item.Value = queryNewBuffers.Where(
                                    (el, key) => 
                                    { return el.Key == key; })
                                .Single()
                                .Value;
            }

            try
            {
                context.BufferItems.AddRange(creatBuffers);
                context.SaveChanges();
            }
            catch (Exception)
            {
                return RequestResult.ServerError;
            }
            return RequestResult.Success;
        }

        /// <summary>
        /// Возвращает экземляр буфера
        /// </summary>
        /// <param name="keyNumber">Номер привязанной клавиши</param>
        /// <returns></returns>
        [HttpGet("{keyNumber}")]
        public WebBuffer Read(int keyNumber)
        {
            User user = _userService.GetUserByClaims(HttpContext.User);
            if (user == null) return null;

            var context = new MultiBufferContext();
            var query =
                from el in context.BufferItems
                where el.Key == keyNumber && el.UserId == user.Id
                select el;
            BufferItem item = query.SingleOrDefault();

            var itemWebApi = new WebBuffer();
            if (item == null) return itemWebApi;

            itemWebApi.Key = item.Key;
            itemWebApi.Name = item.Name;
            itemWebApi.Value = item.Value;
            return itemWebApi;
        }

        /// <summary>
        /// Возвращает все буферы пользователя, которые есть в базе
        /// </summary>
        /// <returns>Коллекция буфферов</returns>
        [HttpGet]
        public IEnumerable<WebBuffer> Read()
        {
            User user = _userService.GetUserByClaims(HttpContext.User);
            if (user == null) return null;

            var query =
                from els in user.Buffers
                select new WebBuffer
                {
                    Name = els.Name,
                    Key = els.Key,
                    Value = els.Value,
                };

            return query;
        }

        /// <summary>
        /// Обновляет экземпляр указанного буфера
        /// </summary>
        /// <param name="keyNumber">Клавиша, привязанная к буферу</param>
        /// <param name="bufferItem">Экземпляр буфера с обновленными данными</param>
        /// <returns></returns>
        [HttpPut]
        public IActionResult Update(WebBuffer bufferItem)
        {
            User user = _userService.GetUserByClaims(HttpContext.User);
            if (user == null) return RequestResult.ClientError;

            var context = new MultiBufferContext();
            var query =
                from el in context.BufferItems
                where el.Key == bufferItem.Key && el.UserId == user.Id
                select el;
            BufferItem item = query.SingleOrDefault();

            if (item == null) return RequestResult.ClientError;

            try
            {
                item.Value = bufferItem.Value;
                context.SaveChanges();
            }
            catch (Exception)
            {
                return RequestResult.ServerError;
            }
            return RequestResult.Success;
        }

        /// <summary>
        /// Удалить указанный буфер
        /// </summary>
        /// <param name="keyNumber">Клавиша, привязнная к буферу</param>
        /// <returns></returns>
        [HttpDelete("{keyNumber}")]
        public IActionResult Delete(int keyNumber)
        {
            User user = _userService.GetUserByClaims(HttpContext.User);
            if (user == null) return RequestResult.ClientError;

            var context = new MultiBufferContext();
            var query =
                from el in context.BufferItems
                where el.Key == keyNumber && el.UserId == user.Id
                select el;
            BufferItem item = query.SingleOrDefault();

            if (item == null) { return RequestResult.ClientError; }

            try
            {
                context.BufferItems.Remove(item);
                context.SaveChanges();
            }
            catch (Exception)
            {
                return RequestResult.ServerError;
            }
            return RequestResult.Success;
        }

        readonly IUserService _userService;
    }
}