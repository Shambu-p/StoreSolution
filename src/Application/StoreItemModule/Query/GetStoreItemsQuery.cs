using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using StoreSolution.Application.common.Interfaces;
using StoreSolution.Domain.Entity;

namespace StoreSolution.Application.StoreItemModule.Query {

    public class GetStoreItemsQuery : IRequest<Store> {

        public uint StoreId {get; init;}

        public GetStoreItemsQuery(uint store_id){
            this.StoreId = store_id;
        }

    }

    public class GetStoreItemsHandler : IRequestHandler<GetStoreItemsQuery, Store> {

        private readonly IDBContext context;

        public GetStoreItemsHandler(IDBContext db_context){
            context = db_context;
        }

        public async Task<Store> Handle(GetStoreItemsQuery request, CancellationToken cancellationToken) {
            
            Store st = context.Stores
                        .Include(s => s.StoreItems)
                        .ThenInclude(store_item => store_item.Item)
                        .Where(s => s.Id == request.StoreId).FirstOrDefault();

            if(st == null){
                throw new Exception("store not found!");
            }

            return st;

        }

    }
    
}