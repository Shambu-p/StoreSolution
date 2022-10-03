using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using StoreBackendClean.Domain.Entity;
using StoreBackendClean.Domain.common;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace StoreBackendClean.Infrastructure.Identity
{
    public class IdentityService {

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
    }
}