using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultiBuffer.WebApi.DataModels;
using System.Security.Claims;

namespace MultiBuffer.WebApi.Utils
{
    public interface IUserService
    {
        User Authenticate(string username, string password);

        User GetUserByClaims(ClaimsPrincipal claims);
    }
}
