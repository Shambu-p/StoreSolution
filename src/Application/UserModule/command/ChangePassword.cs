using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using StoreSolution.Application.common.Interfaces;
using StoreSolution.Domain.Entity;

namespace StoreSolution.Application.UserModule.command
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

        private readonly IDBContext context;

        public ChangePasswordHandler(IDBContext db_context) {
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

            await context.SaveChangesAsync(cancellationToken);

            return found_user;

        }
    }
}