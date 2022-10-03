using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using StoreSolution.Domain.Entity;
using StoreSolution.Application.StoreModule.command;
using StoreSolution.Application.StoreModule.Query;
using StoreSolution.Application.StoreItemModule.command;
using StoreSolution.Application.StoreItemModule.Query;
using Microsoft.AspNetCore.Authorization;

namespace StoreSolution.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StoreController : ApiController {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Store>>> getAllStores() {

            try{
                return Ok(await mediator.Send(new GetAllStoresQuery()));
            }catch(Exception ex){
                return NotFound(ex.Message);
            }

        }

        [HttpGet("items/{store_id}")]
        public async Task<ActionResult<IEnumerable<StoreItem>>> getAllStoreItems(uint store_id) {

            try{
                return Ok(await mediator.Send(new GetStoreItemsQuery(store_id)));
            }catch(Exception ex){
                return NotFound(ex.Message);
            }

        }

        [HttpGet("{store_id}")]
        public async Task<ActionResult<Store>> getStore(uint store_id){

            try{
                return Ok(await mediator.Send(new GetStoreQuery(store_id)));
            }catch(Exception ex){
                return NotFound(ex.Message);
            }

        }

        [HttpGet("items/single_item/{id}")]
        public async Task<ActionResult<IEnumerable<StoreItem>>> getStoreItem(uint id) {

            try{
                return Ok(await mediator.Send(new GetStoreItemQuery(id)));
            }catch(Exception ex){
                return NotFound(ex.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult<Store>> addStore([FromForm] string store_name, [FromForm] uint store_keeper) {

            try{
                return Ok(await mediator.Send(new CreateStore(store_name, store_keeper)));
            }catch(Exception ex){
                return NotFound(ex.Message);
            }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                

        }

        [HttpPost("items")]
        public async Task<ActionResult<StoreItem>> createStoreItem([FromForm] uint store_id, [FromForm] uint item_id, [FromForm] uint amount) {

            try{
                return Ok(await mediator.Send(new CreateStoreItem(store_id, item_id, amount)));
            }catch(Exception ex){
                return NotFound(ex.Message);
            }

        }

        [HttpPut]
        public async Task<ActionResult<Store>> editStore([FromForm] uint store_id, [FromForm] string store_name, [FromForm] uint store_keeper) {

            try{
                return Ok(await mediator.Send(new ChangeStore(store_id, store_name, store_keeper)));
            }catch(Exception ex){
                return NotFound(ex.Message);
            }

        }

        [HttpPut("items/change_amount")]
        public async Task<ActionResult<StoreItem>> addStoreItemAmount([FromForm] uint store_id, [FromForm] uint item_id, [FromForm] int amount) {

            try{
                
                StoreItem store_item;
                
                if(amount > 0){
                    store_item = await mediator.Send(new ChangeStoreItemQuantity(store_id, item_id, Convert.ToUInt32(amount), true));
                }else{
                    store_item = await mediator.Send(new ChangeStoreItemQuantity(store_id, item_id, Convert.ToUInt32(Math.Abs(amount)), false));
                }

                return Ok(store_item);

            }catch(Exception ex){
                return NotFound(ex.Message);
            }

        }

    }

}