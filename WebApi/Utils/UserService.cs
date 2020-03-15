using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultiBuffer.WebApi.DataModels;

namespace MultiBuffer.WebApi.Utils
{
    public class UserService : IUserService
    {
        public User Authenticate(string username, string password)
        {
            return new User();
        }
    }
}
