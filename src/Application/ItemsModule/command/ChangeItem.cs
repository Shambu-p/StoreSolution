using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using StoreSolution.Application.common.Interfaces;
using StoreSolution.Domain.Entity;

namespace StoreSolution.Application.ItemsModule.command
{
    public class ChangeItem : IRequest<Item> {
        
        public uint Id {get; init;}
        public string Name {get; init;}
        public double Price {get; init;}

        public ChangeItem(uint id, string name, double price){
            this.Id = id;
            this.Name = name;
            this.Price = price;
        }

    }

    public class ChangeItemHandler : IRequestHandler<ChangeItem, Item> {

        private readonly IDBContext context;

        public ChangeItemHandler(IDBContext db_context){
            context = db_context;
        }

        public async Task<Item> Handle(ChangeItem request, CancellationToken cancellationToken) {
        
            Item item = await context.Items.FindAsync(request.Id);

            if(item != null) {
                throw new Exception("Item not found!");
            }

            item.Name = request.Name;
            item.Price = request.Price;

            context.Entry(item).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return item;

        }

    }
}