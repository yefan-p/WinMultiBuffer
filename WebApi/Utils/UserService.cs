using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultiBuffer.WebApi.DataModels;
using Microsoft.Extensions.Options;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace MultiBuffer.WebApi.Utils
{
    public class UserService : IUserService
    {
        public UserService(IOptions<AppSettings> appSettings, MultiBufferContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        /// <summary>
        /// Если пользователь существует, выдает ему токен
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        /// <param name="password">Пароль</param>
        /// <returns></returns>
        public User Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var query =
                from el in _context.Users
                where el.Username == username && el.Password == password
                select el;
            User user = query.SingleOrDefault();

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.Password = "";

            return user;
        }

        /// <summary>
        /// Получить пользователя из токена
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public User GetUserByClaims(ClaimsPrincipal claims)
        {
            var queryClaim =
                    from el in claims.Claims
                    where el.Type == ClaimTypes.Name
                    select el;
            Claim claim = queryClaim.SingleOrDefault();

            if (claim == null) return null;
            if (!int.TryParse(claim.Value, out int userId)) return null;

            var queryUser =
                    from el in _context.Users
                    where el.Id == userId
                    select el;
            return queryUser.SingleOrDefault();
        }

        readonly MultiBufferContext _context;

        readonly AppSettings _appSettings;
    }
}
