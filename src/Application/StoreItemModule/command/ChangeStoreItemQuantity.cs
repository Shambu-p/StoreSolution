using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using StoreSolution.Application.common.Interfaces;
using StoreSolution.Domain.Entity;

namespace StoreSolution.Application.StoreItemModule.command
{
    public class ChangeStoreItemQuantity : IRequest<StoreItem> {

        public uint StoreId {get; init;}
        public uint ItemId {get; init;}
        public uint Amount {get; init;}
        public bool IsMaximize {get; init;}

        public ChangeStoreItemQuantity(uint store_id, uint item_id, uint new_amount, bool is_maximize) {
            this.StoreId = store_id;
            this.ItemId = item_id;
            this.Amount = new_amount;
            this.IsMaximize = is_maximize;
        }
        
    }

    public class ChangeStoreItemQuantityHandler : IRequestHandler<ChangeStoreItemQuantity, StoreItem> {

        private readonly IDBContext context;

        public ChangeStoreItemQuantityHandler(IDBContext db_context){
            context = db_context;
        }

        public async Task<StoreItem> Handle(ChangeStoreItemQuantity request, CancellationToken cancellationToken) {
            
            StoreItem store_item = context.StoreItems
                                    .Where(st => st.ItemId == request.ItemId && st.StoreId == request.StoreId)
                                    .FirstOrDefault();

            if(request.IsMaximize){
            
                store_item.TotalAmount = store_item.TotalAmount + request.Amount;
                store_item.UnboxedAmount = store_item.UnboxedAmount + request.Amount;
            
            }else{

                if(request.Amount > store_item.UnboxedAmount){
                    throw new Exception("trying to export more number of Items than there is available number inside store");
                }

                store_item.TotalAmount = store_item.TotalAmount - request.Amount;
                store_item.UnboxedAmount = store_item.UnboxedAmount - request.Amount;

            }

            this.context.SaveChangesAsync(cancellationToken);
            return store_item;

        }

    }

}