using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StoreSolution.Domain.Entity;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StoreSolution.Application.ItemsModule.command;
using StoreSolution.Application.ItemsModule.Query;
using Microsoft.AspNetCore.Authorization;

namespace StoreSolution.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ItemController : ApiController {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> all(){
            
            try{
                return Ok(await mediator.Send(new GetAllItemsQuery()));
            }catch(Exception ex){
                return NotFound(ex.Message);
            }

        }

        [HttpGet("{item_id}")]
        public async Task<ActionResult<Item>> getItem(uint item_id) {
            
            try{
                return Ok(await mediator.Send(new GetItemQuery(item_id)));
            }catch(Exception ex){
                return NotFound(ex.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult<Item>> addItem([FromForm] string name, [FromForm] double price){
            
            try{
                return Ok(await mediator.Send(new CreateItem(name, price)));
            }catch(Exception ex){
                return NotFound(ex.Message);
            }

        }

        [HttpPut]
        public async Task<ActionResult<Item>> changeItem([FromForm] uint id, [FromForm] string name, [FromForm] double price){
            
            try{
                return Ok(await mediator.Send(new ChangeItem(id, name, price)));
            }catch(Exception ex){
                return NotFound(ex.Message);
            }

        }

    }

}