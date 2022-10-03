using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using StoreSolution.Application.common.Interfaces;
using StoreSolution.Domain.Entity;

namespace StoreSolution.Application.UserModule.Query {
    
    public class GetUserQuery : IRequest<User> {
        
        public uint id {get; init;}

        public GetUserQuery(uint id){
            this.id = id;
        }

    }

    public class GetUserHandler : IRequestHandler<GetUserQuery, User> {

        private readonly IDBContext context;

        public GetUserHandler(IDBContext db_context){
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