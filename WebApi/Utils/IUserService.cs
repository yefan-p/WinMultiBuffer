using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultiBuffer.WebApi.DataModels;

namespace MultiBuffer.WebApi.Utils
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
    }
}
