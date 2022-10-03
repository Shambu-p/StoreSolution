using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using StoreBackendClean.Infrastructure.Persistance;
using StoreBackendClean.Domain.Entity;

namespace StoreBackendClean.Application.UserModule.Query {
    
    public class GetUserQuery : IRequest<User> {
        
        public uint id {get; init;}

        public GetUserQuery(uint id){
            this.id = id;
        }

    }

    public class GetUserHandler : IRequestHandler<GetUserQuery, User> {

        private readonly ApplicationContext context;

        public GetUserHandler(ApplicationContext db_context){
            context = db_context;
        }

        public async Task<User> Handle(GetUserQuery request, CancellationToken cancellationToken) {

            var result = await context.Users.FindAsync(request.id);
            
            if(result == null){
                throw new Exception("user not found");
            }

            return result;

        }

    }
}