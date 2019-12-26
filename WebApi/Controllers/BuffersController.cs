using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DAL.Models;
using DAL.Repos;

namespace MultiBuffer.WebApi.Controllers
{
    [RoutePrefix("api/Buffers")]
    public class BuffersController : ApiController
    {
        public BuffersController()
        {
            _buffersRepo = new BuffersRepo();
        }

        private BuffersRepo _buffersRepo;

        // GET: api/Buffers/5
        [HttpGet, Route("{idUser}")]
        public IEnumerable<BuffersModel> Get(int idUser)
        {
            return _buffersRepo.Read(idUser);
        }
    }
}
