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
    public class CreateItem : IRequest<Item> {

        public string Name {get; init;}
        public double Price {get; init;}

        public CreateItem(string name, double price){
            this.Name = name;
            this.Price = price;
        }

    }

    public class CreateItemHandler : IRequestHandler<CreateItem, Item> {

        private readonly IDBContext context;

        public CreateItemHandler(IDBContext db_context){
            context = db_context;
        }

        public async Task<Item> Handle(CreateItem request, CancellationToken cancellationToken) {
            
            Item new_item = new Item();
            new_item.Name = request.Name;
            new_item.Price = request.Price;

            context.Items.Add(new_item);
            await context.SaveChangesAsync(cancellationToken);

            return new_item;

        }

    }
}