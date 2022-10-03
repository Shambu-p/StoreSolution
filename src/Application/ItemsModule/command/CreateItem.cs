using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using StoreBackendClean.Infrastructure.Persistance;
using StoreBackendClean.Domain.Entity;

namespace StoreBackendClean.Application.ItemsModule.command
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

        private readonly ApplicationContext context;

        public CreateItemHandler(ApplicationContext db_context){
            context = db_context;
        }

        public async Task<Item> Handle(CreateItem request, CancellationToken cancellationToken) {
            
            Item new_item = new Item();
            new_item.Name = request.Name;
            new_item.Price = request.Price;

            context.Items.Add(new_item);
            await context.SaveChangesAsync();

            return new_item;

        }

    }
}