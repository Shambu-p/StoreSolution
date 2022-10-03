using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreSolution.Domain.Entity;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

using MediatR;
using StoreSolution.Application.UserModule.command;
using StoreSolution.Application.UserModule.Query;

namespace StoreSolution.WebAPI.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ApiController {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> all() {
            try{
                return Ok(await mediator.Send(new GetUsersQuery()));
            }catch(Exception ex){
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> single(uint id) {

            try{
                return Ok(await mediator.Send(new GetUserQuery(id)));
            }catch(Exception ex){
                return NotFound(ex.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult<User>> add([FromForm] string name,[FromForm] string email,[FromForm] string password,[FromForm] byte role) {

            try{
                return Ok(await mediator.Send(new CreateUser(name, email, role, password)));
            }catch(Exception ex){
                return NotFound(ex.Message);
            }

        }

        [HttpPut]
        public async Task<ActionResult<User>> change([FromForm]uint id,[FromForm] string name,[FromForm] string email,[FromForm] byte role) {

            try {

                User new_user = new User();
                new_user.Name = name;
                new_user.Id = id;
                new_user.Email = email;
                new_user.Role = role;
                
                return Ok(await mediator.Send(new ChangeUser(new_user)));

            } catch(Exception ex) {
                return NotFound(ex.Message);
            }

        }

        [HttpPut("change_password")]
        public async Task<ActionResult<User>> changePassword([FromForm] string current_password, [FromForm] string new_password, [FromForm] string confirm_password) {
            
            try{
                return Ok(mediator.Send(new ChangePassword(uint.Parse(User?.FindFirstValue("Id")), current_password, new_password, confirm_password)));
            } catch(Exception ex) {
                return NotFound(ex.Message);
            }

        }

    }
}