using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using StoreSolution.Application.common.Interfaces;
using StoreSolution.Domain.Entity;

namespace StoreSolution.Application.ItemsModule.Query
{
    public class GetAllItemsQuery : IRequest<IEnumerable<Item>> {}

    public class GetAllItemsHandler : IRequestHandler<GetAllItemsQuery, IEnumerable<Item>> {

        private readonly IDBContext context;

        public GetAllItemsHandler(IDBContext db_context){
            context = db_context;
        }

        public async Task<IEnumerable<Item>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken) {
            return await context.Items.ToListAsync();
        }

    }
}