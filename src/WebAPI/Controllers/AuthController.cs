using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
// using StoreSolution.Domain.Entity;
using StoreSolution.Application.common.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

using StoreSolution.Application.Auth.command;


namespace StoreSolution.WebAPI.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ApiController {

        [HttpPost("login")]
        public async Task<ActionResult<UserAuthentication>> Authenticate([FromBody] LoginCommand command) {

            try{
                return Ok(await mediator.Send(command));
            }catch(Exception ex){
                return NotFound(ex.Message);
            }

        }

        [HttpGet("authorization"), Authorize]
        public ActionResult<UserAuthentication> authorization(){

            UserAuthentication user_return = new UserAuthentication();
            user_return.Id = uint.Parse(User?.FindFirstValue("Id"));
            user_return.Name = User?.FindFirstValue("Name");
            user_return.Email = User?.FindFirstValue("Email");

            // string role = User?.FindFirstValue("Role");
            user_return.Role = Convert.ToByte(User?.FindFirstValue("Role"));
            // user_return.Token = token;

            return user_return;

        }
    }

}