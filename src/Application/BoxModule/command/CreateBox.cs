using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using StoreBackendClean.Infrastructure.Persistance;
using StoreBackendClean.Domain.Entity;

namespace StoreBackendClean.Application.BoxModule.command
{
    public class CreateBox : IRequest<Box> {

        public uint StoreId {get; init;}
        public uint CreatorUser {get; init;}

        public CreateBox(uint store_id, uint creator_user){
            this.StoreId = store_id;
            this.CreatorUser = creator_user;
        }

    }

    public class CreateBoxHandler : IRequestHandler<CreateBox, Box> {

        private readonly ApplicationContext context;

        public CreateBoxHandler(ApplicationContext db_context){
            context = db_context;
        }

        public async Task<Box> Handle(CreateBox request, CancellationToken cancellationToken) {

            User user = await context.Users.FindAsync(request.CreatorUser);
            Store store = await context.Stores.FindAsync(request.StoreId);

            if(user == null) {
                throw new Exception("Creator Not found!");
            }

            if(store == null) {
                throw new Exception("Store not found! Unknown Store");
            }

            Box new_box = new Box();
            new_box.UserId = request.CreatorUser;
            new_box.StoreId = request.StoreId;

            context.Boxes.Add(new_box);
            await context.SaveChangesAsync();

            return new_box;

        }

    }

}