using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using StoreBackendClean.Infrastructure.Persistance;
using StoreBackendClean.Domain.Entity;

namespace StoreBackendClean.Application.StoreModule.Query
{
    public class GetAllStoresQuery : IRequest<IEnumerable<Store>> {}

    public class GetAllStoresHandler : IRequestHandler<GetAllStoresQuery, IEnumerable<Store>> {

        private readonly ApplicationContext context;

        public GetAllStoresHandler(ApplicationContext db_context){
            context = db_context;
        }

        public async Task<IEnumerable<Store>> Handle(GetAllStoresQuery request, CancellationToken cancellationToken) {
            return await context.Stores.ToListAsync();
        }

    }
}