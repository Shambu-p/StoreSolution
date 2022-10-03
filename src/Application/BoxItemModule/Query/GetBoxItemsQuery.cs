using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using StoreSolution.Application.common.Interfaces;
using StoreSolution.Domain.Entity;

namespace StoreSolution.Application.BoxItemModule.Query
{
    public class GetBoxItemsQuery : IRequest<IEnumerable<BoxItem>> {
        
        public uint BoxId {get; init;}
        public GetBoxItemsQuery(uint box_id){
            this.BoxId = box_id;
        }

    }

    public class GetBoxItemsHandler : IRequestHandler<GetBoxItemsQuery, IEnumerable<BoxItem>> {

        private readonly IDBContext context;

        public GetBoxItemsHandler(IDBContext db_context){
            context = db_context;
        }

        public async Task<IEnumerable<BoxItem>> Handle(GetBoxItemsQuery request, CancellationToken cancellationToken) {
            return await context.BoxItems.Include(bs => bs.Item).Where((b => b.BoxId == request.BoxId)).ToListAsync();
        }

    }
}