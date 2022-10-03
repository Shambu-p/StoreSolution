using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using StoreSolution.Application.common.Interfaces;
using StoreSolution.Domain.Entity;

namespace StoreSolution.Application.BoxItemModule.command
{
    public class ChangeItemQuantity : IRequest<BoxItem> {
        
        public uint ItemId {get; init;}
        public uint BoxId {get; init;}
        public int Amount {get; init;}
        public bool IsMaximize {get; init;}

        public ChangeItemQuantity(uint item_id, uint box_id, int amount, bool is_maximize){
            this.ItemId = item_id;
            this.BoxId = box_id;
            this.IsMaximize = is_maximize;
            this.Amount = amount;
        }
    }

    public class ChangeItemQuantityHandler : IRequestHandler<ChangeItemQuantity, BoxItem> {

        private readonly IDBContext context;

        public ChangeItemQuantityHandler(IDBContext db_context){
            context = db_context;
        }

        public async Task<BoxItem> Handle(ChangeItemQuantity request, CancellationToken cancellationToken) {

            Box bx = context.Boxes
                .Include(b => b.Store)
                .Where(b => b.Id == request.BoxId)
                .FirstOrDefault();
            
            if(bx == null){
                throw new Exception("box not found!");
            }

            StoreItem st = context.StoreItems
                .Where(st => st.ItemId == request.ItemId && st.StoreId == bx.Store.Id)
                .FirstOrDefault<StoreItem>();
            
            if(st == null){
                throw new Exception("Item not found in the store!");
            }

            if(request.IsMaximize && (st.UnboxedAmount == 0 || st.UnboxedAmount < request.Amount)){
                throw new Exception("trying to box more number of items than the available unboxed number of items in store");
            }

            BoxItem bx_item = context.BoxItems
                .Where(b => b.BoxId == request.BoxId && b.ItemId == request.ItemId)
                .FirstOrDefault();

            if(bx_item == null){
                throw new Exception("box item not found!");
            }

            if(!request.IsMaximize && (bx_item.Amount >= request.Amount)) {
                throw new Exception("trying to unbox more number of items than the available boxed number of items in side the box");
            }

            if(request.IsMaximize){
                bx_item.Amount += request.Amount;
                st.UnboxedAmount -= Convert.ToUInt32(request.Amount);
            }
            else{
                bx_item.Amount -= request.Amount;
                st.UnboxedAmount += Convert.ToUInt32(request.Amount);
            }

            // await store_item_service.itemBoxing(request.ItemId, bx.Store.Id, Convert.ToUInt32(request.Amount));
            await context.SaveChangesAsync();
            return bx_item;

        }

    }
}