using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using StoreSolution.Application.common.Interfaces;
using StoreSolution.Domain.Entity;

namespace StoreSolution.Application.StoreItemModule.Query
{
    public class GetStoreItemQuery : IRequest<StoreItem> {
        
        public uint Id {get; init;}

        public GetStoreItemQuery(uint id){
            this.Id = id;
        }

    }

    public class GetStoreItemHandler : IRequestHandler<GetStoreItemQuery, StoreItem> {

        private readonly IDBContext context;

        public GetStoreItemHandler(IDBContext db_context){
            context = db_context;
        }

        public async Task<StoreItem> Handle(GetStoreItemQuery request, CancellationToken cancellationToken) {
            
            StoreItem item = context.StoreItems
                .Include(si => si.Store)
                .Include(si => si.Item)
                .Where(si => si.Id == request.Id)
                .FirstOrDefault();
            if(item == null){
                throw new Exception("Item not found in the store");
            }
            return item;

        }

    }
}