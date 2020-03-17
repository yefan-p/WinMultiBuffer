using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MultiBuffer.IWebApi;
using MultiBuffer.WebApi.Utils;
using MultiBuffer.WebApi.DataModels;

namespace MultiBuffer.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("auth")]
        public IActionResult Authenticate(AuthenticateUser model)
        {
            User user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return RequestResult.ClientError;

            model.Password = "";
            model.Token = user.Token;

            return Ok(model);
        }

        IUserService _userService;
    }
}