using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using StoreSolution.Application.common.Interfaces;
using StoreSolution.Domain.Entity;

namespace StoreSolution.Application.StoreModule.Query
{
    public class GetStoreQuery : IRequest<Store> {
        
        public uint Id {get; init;}

        public GetStoreQuery(uint id){
            this.Id = id;
        }

    }

    public class GetStoreHandler : IRequestHandler<GetStoreQuery, Store> {

        private readonly IDBContext context;

        public GetStoreHandler(IDBContext db_context){
            context = db_context;
        }

        public async Task<Store> Handle(GetStoreQuery request, CancellationToken cancellationToken) {
            
            var store = await context.Stores.FindAsync(request.Id);

            if(store == null){
                throw new Exception("Store not found");
            }

            return store;

        }

    }
}