using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using StoreSolution.Application.common.Interfaces;
using StoreSolution.Domain.Entity;

namespace StoreSolution.Application.ItemsModule.Query
{
    public class GetItemQuery : IRequest<Item> {
        
        public uint Id {get; init;}

        public GetItemQuery(uint id){
            this.Id = id;
        }

    }

    public class GetItemHandler : IRequestHandler<GetItemQuery, Item> {

        private readonly IDBContext context;

        public GetItemHandler(IDBContext db_context){
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