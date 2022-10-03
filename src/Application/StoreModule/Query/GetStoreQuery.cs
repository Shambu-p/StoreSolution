using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using StoreBackendClean.Infrastructure.Persistance;
using StoreBackendClean.Domain.Entity;

namespace StoreBackendClean.Application.StoreModule.Query
{
    public class GetStoreQuery : IRequest<Store> {
        
        public uint Id {get; init;}

        public GetStoreQuery(uint id){
            this.Id = id;
        }

    }

    public class GetStoreHandler : IRequestHandler<GetStoreQuery, Store> {

        private readonly ApplicationContext context;

        public GetStoreHandler(ApplicationContext db_context){
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