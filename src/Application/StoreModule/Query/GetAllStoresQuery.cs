using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using StoreSolution.Application.common.Interfaces;
using StoreSolution.Domain.Entity;

namespace StoreSolution.Application.StoreModule.Query
{
    public class GetAllStoresQuery : IRequest<IEnumerable<Store>> {}

    public class GetAllStoresHandler : IRequestHandler<GetAllStoresQuery, IEnumerable<Store>> {

        private readonly IDBContext context;

        public GetAllStoresHandler(IDBContext db_context){
            context = db_context;
        }

        public async Task<IEnumerable<Store>> Handle(GetAllStoresQuery request, CancellationToken cancellationToken) {
            return await context.Stores.ToListAsync();
        }

    }
}