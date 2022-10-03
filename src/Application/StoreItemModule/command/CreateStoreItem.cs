using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using StoreBackendClean.Infrastructure.Persistance;
using StoreBackendClean.Domain.Entity;

namespace StoreBackendClean.Application.StoreItemModule.command {

    public class CreateStoreItem : IRequest<StoreItem> {

        public uint StoreId {get; init;}
        public uint ItemId {get; init;}
        public uint Amount {get; init;}

        public CreateStoreItem(uint store_id, uint item_id, uint amount){
            this.StoreId = store_id;
            this.ItemId = item_id;
            this.Amount = amount;
        }

    }

    public class CreateStoreItemHandler : IRequestHandler<CreateStoreItem, StoreItem> {

        private readonly ApplicationContext context;

        public CreateStoreItemHandler(ApplicationContext db_context){
            context = db_context;
        }

        public async Task<StoreItem> Handle(CreateStoreItem request, CancellationToken cancellationToken) {
        
            Store store = context.Stores.Include(s => s.StoreItems).Where(s => s.Id == request.StoreId).FirstOrDefault();
            
            Item item = await context.Items.FindAsync(request.ItemId);

            if(store == null){
                throw new Exception("store not found");
            }

            if(item == null){
                throw new Exception("item not found");
            }

            StoreItem new_store_item = new StoreItem();
            new_store_item.StoreId = request.StoreId;
            new_store_item.ItemId = request.ItemId;
            new_store_item.TotalAmount = request.Amount;
            new_store_item.UnboxedAmount = request.Amount;

            store.StoreItems.Add(new_store_item);
            await context.SaveChangesAsync();

            new_store_item.Item = item;

            return new_store_item;

        }

    }
}