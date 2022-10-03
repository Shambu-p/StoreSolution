using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using StoreSolution.Domain.Entity;
using StoreSolution.Application.BoxModule.command;
using StoreSolution.Application.BoxModule.Query;
using StoreSolution.Application.BoxItemModule.Query;
using StoreSolution.Application.BoxItemModule.command;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Security.Claims;

namespace StoreSolution.WebAPI.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BoxController : ApiController {

        [HttpGet("{id}")]
        public async Task<ActionResult<Box>> getBox(uint id) {

            try{
                return Ok(await mediator.Send(new GetBoxQuery(id)));
            }catch(Exception ex){
                return NotFound(ex.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult<Box>> saveBox([FromForm] uint store_id){

            try{
                return Ok(await mediator.Send(new CreateBox(store_id, uint.Parse(User?.FindFirstValue("Id")))));
            }catch(Exception ex){
                return NotFound(ex.Message);
            }

        }

        [HttpGet("items/{box_id}/{item_id}")]
        public async Task<ActionResult<BoxItem>> boxItem(uint box_id, uint item_id){
            
            try{
                return Ok(await mediator.Send(new GetBoxItemQuery(box_id, item_id)));
            }catch(Exception ex){
                return NotFound(ex.Message);
            }

        }

        [HttpGet("items/{box_id}")]
        public async Task<ActionResult<IEnumerable<BoxItem>>> getByBox(uint box_id){
            
            try{
                return Ok(await mediator.Send(new GetBoxItemsQuery(box_id)));
            }catch(Exception ex){
                return NotFound(ex.Message);
            }

        }

        [HttpPost("items")]
        public async Task<ActionResult<BoxItem>> createBoxItem([FromForm] uint box_id, [FromForm] uint item_id, [FromForm] int amount) {

            try{
                return Ok(await mediator.Send(new CreateBoxItem(box_id, item_id, amount)));
            }catch(Exception ex){
                return NotFound(ex.Message);
            }

        }

        [HttpPut("items/change_quantity")]
        public async Task<ActionResult<BoxItem>> addBoxItem([FromForm] uint box_id,[FromForm] uint item_id,[FromForm] int amount) {

            try{
                
                ChangeItemQuantity command;
                
                if(amount > 0){
                    command = new ChangeItemQuantity(item_id, box_id, amount, true);
                } else{
                    command = new ChangeItemQuantity(item_id, box_id, Math.Abs(amount), false);
                }

                return Ok(await mediator.Send(command));

            }catch(Exception ex){
                return NotFound(ex.Message);
            }

        }

    }
}