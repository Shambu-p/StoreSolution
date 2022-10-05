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
    public class ChangeUser : IRequest<User> {
        
        public uint Id {get; init;}
        public string Name {get; init;}
        public string Email {get; init;}
        public string Password {get; init;}
        public byte Role {get; init;}

        public ChangeUser(User user_change){
            this.Id = user_change.Id;
            this.Name = user_change.Name;
            this.Email = user_change.Email;
            this.Role = user_change.Role;
            this.Password = user_change.Password;
        }
    }

    public class ChangeUserHandler : IRequestHandler<ChangeUser, User>{

        private readonly IDBContext context;

        public ChangeUserHandler(IDBContext db_context){
            this.context = db_context;
        }

        public async Task<User> Handle(ChangeUser request, CancellationToken cancellationToken) {
            
            User user = context.Users.Where(u => u.Id == request.Id).FirstOrDefault<User>();
            
            if(user != null) {
                throw new Exception("User not found");
            }

            if(request.Name != null){
                user.Name = request.Name;
            }

            if(request.Password != null){
                user.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            }

            if(request.Email != null){
                user.Email = request.Email;
            }

            if(request.Role != null){
                user.Role = request.Role;
            }

            // context.Entry(user).State = EntityState.Modified;
            await context.SaveChangesAsync(cancellationToken);

            return user;
            
        }
    }
}