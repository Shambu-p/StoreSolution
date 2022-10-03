using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using StoreSolution.Application.common.Models;
using StoreSolution.Application.common.Interfaces;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace StoreSolution.Infrastructure.Identity
{
    public class IdentityService: IIdentityService {

        public readonly AuthSettings jwtSettings;

        public IdentityService(IOptions<AuthSettings> option) {
            this.jwtSettings = option.Value;
        }

        public UserAuthentication generateIdentity(UserAuthentication user){

            var token_descriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new Claim[]{
                        new Claim("Id", user.Id.ToString()),
                        new Claim("Name", user.Name),
                        new Claim("Email", user.Email),
                        new Claim("Role", user.Role.ToString())
                    }),

                Expires = DateTime.Now.AddSeconds(3600),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.jwtSettings.securityKey)),
                    SecurityAlgorithms.HmacSha256
                )
            };

            var token_handler = new JwtSecurityTokenHandler();
            user.Token = token_handler.WriteToken(token_handler.CreateToken(token_descriptor));

            return user;

        }

        public UserAuthentication getUser(){

            UserAuthentication user_return = new UserAuthentication();
            // user_return.Id = uint.Parse(User?.FindFirstValue("Id"));
            // user_return.Name = User?.FindFirstValue("Name");
            // user_return.Email = User?.FindFirstValue("Email");

             // string role = User?.FindFirstValue("Role");
            // user_return.Role = Convert.ToByte(User?.FindFirstValue("Role"));
             // user_return.Token = token;

            return user_return;

        }
    }
}