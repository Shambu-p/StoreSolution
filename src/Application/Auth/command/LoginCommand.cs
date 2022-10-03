using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using StoreSolution.Domain.Entity;
using StoreSolution.Application.common.Interfaces;
using StoreSolution.Application.common.Models;
// using Microsoft.Extensions.Options;


namespace StoreSolution.Application.Auth.command {

    public class LoginCommand : IRequest<UserAuthentication> {

        public string Email {get; init;}
        public string Password {get; init;}

        public LoginCommand(string email, string password){
            this.Email = email;
            this.Password = password;
        }

    }

    public class LoginHandler : IRequestHandler<LoginCommand, UserAuthentication> {

        private readonly IDBContext context;
        private readonly IIdentityService identityService;

        public LoginHandler(IDBContext db_context, IIdentityService identity) { //IOptions<AuthSettings> option
            context = db_context;
            identityService = identity;
            // identityService = new IdentityService(option);
        }

        public async Task<UserAuthentication> Handle(LoginCommand request, CancellationToken cancellationToken) {

            User user = context.Users.Where(u => u.Email == request.Email).FirstOrDefault();

            if(user == null) {
                throw new Exception("User Not found!");
            }

            UserAuthentication user_return = new UserAuthentication();
            user_return.Id = user.Id;
            user_return.Name = user.Name;
            user_return.Email = user.Email;
            user_return.Role = user.Role;
            user_return.Token = null;

            return identityService.generateIdentity(user_return);

        }

    }

}