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
    public class CreateBoxItem : IRequest<BoxItem> {

        public uint ItemId {get; init;}
        public uint BoxId {get; init;}
        public int Amount {get; init;}

        public CreateBoxItem(uint box_id, uint item_id, int amount){
            this.ItemId = item_id;
            this.BoxId = box_id;
            this.Amount = amount;
        }

    }

    public class CreateBoxItemHandler : IRequestHandler<CreateBoxItem, BoxItem> {

        private readonly IDBContext context;

        public CreateBoxItemHandler(IDBContext db_context){
            context = db_context;
        }

        public async Task<BoxItem> Handle(CreateBoxItem request, CancellationToken cancellationToken) {

            Box bx = context.Boxes
                .Include(b => b.Store)
                .Where(b => b.Id == request.BoxId)
                .FirstOrDefault();
            
            if(bx == null){
                throw new Exception("box not found!");
            }

            StoreItem st = context.StoreItems
                .Include(si => si.Item)
                .Where(si => si.ItemId == request.ItemId && si.StoreId == bx.Store.Id)
                .FirstOrDefault<StoreItem>();
            
            if(st.UnboxedAmount == 0 || st.UnboxedAmount < request.Amount){
                throw new Exception("trying to box more number of items than the available number of items in store");
            }

            BoxItem bx_item = new BoxItem();
            bx_item.BoxId = request.BoxId;
            bx_item.ItemId = request.ItemId;
            bx_item.Amount = request.Amount;

            // await store_item_service.itemBoxing(item_id, bx.Store.Id, Convert.ToUInt32(amount));
            st.UnboxedAmount -= Convert.ToUInt32(request.Amount);
            context.BoxItems.Add(bx_item);
            await context.SaveChangesAsync(cancellationToken);

            bx_item.Item = st.Item;

            return bx_item;

        }

    }
}