using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using StoreBackendClean.Infrastructure.Persistance;
using StoreBackendClean.Domain.Entity;

namespace StoreBackendClean.Application.ItemsModule.Query
{
    public class GetAllItemsQuery : IRequest<IEnumerable<Item>> {}

    public class GetAllItemsHandler : IRequestHandler<GetAllItemsQuery, IEnumerable<Item>> {

        private readonly ApplicationContext context;

        public GetAllItemsHandler(ApplicationContext db_context){
            context = db_context;
        }

        public async Task<IEnumerable<Item>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken) {
            return await context.Items.ToListAsync();
        }

    }
}