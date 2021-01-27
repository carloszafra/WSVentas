using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSVentas.Models.Requests;
using WSVentas.Models.Responses;
using WSVentas.Models;
using WSVentas.Tools;
using WSVentas.Models.Common;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace WSVentas.Services
{
    public class UserService : Iuser
    {
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            this._appSettings = appSettings.Value;
        }
        public UserResponse Auth(AuthRequest req)
        {
            UserResponse userResponse = new UserResponse();
            using(var db = new Sales_DBContext())
            {
                string spassword = Encrypt.GetSHA256(req.Password);

                var user = db.Users.Where(d => d.Email == req.Email &&
                                          d.Password == spassword).FirstOrDefault();
               
                if (user == null) return null;

                userResponse.Email = user.Email;
                userResponse.Token = GetToken(user);
            }

            return userResponse;
        }

        private string GetToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                      new Claim[]
                      {
                          new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                          new Claim(ClaimTypes.Email, user.Email)
                      }
                    ),
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
