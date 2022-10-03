using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using StoreBackendClean.Infrastructure.Persistance;
using StoreBackendClean.Domain.Entity;

namespace StoreBackendClean.Application.StoreItemModule.Query {

    public class GetStoreItemsQuery : IRequest<Store> {

        public uint StoreId {get; init;}

        public GetStoreItemsQuery(uint store_id){
            this.StoreId = store_id;
        }

    }

    public class GetStoreItemsHandler : IRequestHandler<GetStoreItemsQuery, Store> {

        private readonly ApplicationContext context;

        public GetStoreItemsHandler(ApplicationContext db_context){
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