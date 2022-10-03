using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using StoreSolution.Application.common.Interfaces;
using StoreSolution.Domain.Entity;

namespace StoreSolution.Application.UserModule.Query
{
    public class GetUsersQuery : IRequest<IEnumerable<User>>{};

    public class GetUserQueryHandler : IRequestHandler<GetUsersQuery, IEnumerable<User>>{

        private readonly IDBContext context;

        public GetUserQueryHandler(IDBContext db_context){
            this.context = db_context;
        }

        public async Task<IEnumerable<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken){
            return await context.Users.ToListAsync();
        }
    }
}