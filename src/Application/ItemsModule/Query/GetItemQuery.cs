using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using StoreBackendClean.Infrastructure.Persistance;
using StoreBackendClean.Domain.Entity;

namespace StoreBackendClean.Application.ItemsModule.Query
{
    public class GetItemQuery : IRequest<Item> {
        
        public uint Id {get; init;}

        public GetItemQuery(uint id){
            this.Id = id;
        }

    }

    public class GetItemHandler : IRequestHandler<GetItemQuery, Item> {

        private readonly ApplicationContext context;

        public GetItemHandler(ApplicationContext db_context){
            context = db_context;
        }

        public async Task<Item> Handle(GetItemQuery request, CancellationToken cancellationToken) {
            
            var user = await context.Items.FindAsync(request.Id);
            if(user == null){
                throw new Exception("Item not found!");
            }

            return user;

        }

    }
}