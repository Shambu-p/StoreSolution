using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using StoreBackendClean.Infrastructure.Persistance;
using StoreBackendClean.Domain.Entity;

namespace StoreBackendClean.Application.UserModule.command
{
    public class ChangePassword : IRequest<User> {
        
        public uint Id {get; init;}
        public string current_password {get; init;}
        public string new_password {get; init;}
        public string confirm_password {get; init;}

        public ChangePassword(uint id, string current, string newer, string confirm){
            this.Id = id;
            this.current_password = current;
            this.new_password = newer;
            this.confirm_password = confirm;
        }

    }

    public class ChangePasswordHandler : IRequestHandler<ChangePassword, User> {

        private readonly ApplicationContext context;

        public ChangePasswordHandler(ApplicationContext db_context) {
            this.context = db_context;
        }

        public async Task<User> Handle(ChangePassword request, CancellationToken cancellationToken) {
            
            User found_user = await context.Users.FindAsync(request.Id);

            if(found_user == null){
                throw new Exception("user not found!");
            }

            if(!BCrypt.Net.BCrypt.Verify(request.current_password, found_user.Password)) {
                throw new Exception("incorrect password!");
            }

            if(request.new_password != request.confirm_password){
                throw new Exception("new passwords does not match!");
            }

            found_user.Password = BCrypt.Net.BCrypt.HashPassword(request.new_password);

            await context.SaveChangesAsync();

            return found_user;

        }
    }
}