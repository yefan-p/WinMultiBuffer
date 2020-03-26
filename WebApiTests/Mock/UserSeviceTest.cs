using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using MultiBuffer.WebApi.DataModels;
using MultiBuffer.WebApi.Utils;

namespace WebApiTests.Mock
{
    public class UserSeviceTest : IUserService
    {
        public User Authenticate(string username, string password)
        {
            var user = new User
            {
                Username = username,
                Password = password
            };
            return user;
        }

        public User GetUserByClaims(ClaimsPrincipal claims)
        {
            var user = new User
            {
                Username = "test",
                Password = "test",
                Id = 1                
            };
            return user;
        }
    }
}
