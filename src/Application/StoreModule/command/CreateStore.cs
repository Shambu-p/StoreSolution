using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using StoreBackendClean.Infrastructure.Persistance;
using StoreBackendClean.Domain.Entity;

namespace StoreBackendClean.Application.StoreModule.command
{
    public class CreateStore : IRequest<Store> {

        public string Name {get; init;}
        public uint StoreKeeper {get; init;}

        public CreateStore(string name, uint store_keeper){
            this.Name = name;
            this.StoreKeeper = store_keeper;
        }

    }

    public class CreateStoreHandler : IRequestHandler<CreateStore, Store> {

        private readonly ApplicationContext context;

        public CreateStoreHandler(ApplicationContext db_context){
            context = db_context;
        }

        public async Task<Store> Handle(CreateStore request, CancellationToken cancellationToken) {

            User store_user = await context.Users.FindAsync(request.StoreKeeper);

            if(store_user == null) {
                throw new Exception("Store keeper not found");
            }

            Store new_store = new Store();
            new_store.Name = request.Name;
            new_store.StoreKeeper = request.StoreKeeper;

            context.Stores.Add(new_store);
            await context.SaveChangesAsync();

            new_store.StoreKeeperNavigation = store_user;

            return new_store;

        }

    }
}