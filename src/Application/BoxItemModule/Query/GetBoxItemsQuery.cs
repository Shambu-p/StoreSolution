using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using StoreBackendClean.Infrastructure.Persistance;
using StoreBackendClean.Domain.Entity;

namespace StoreBackendClean.Application.BoxItemModule.Query
{
    public class GetBoxItemsQuery : IRequest<IEnumerable<BoxItem>> {
        
        public uint BoxId {get; init;}
        public GetBoxItemsQuery(uint box_id){
            this.BoxId = box_id;
        }

    }

    public class GetBoxItemsHandler : IRequestHandler<GetBoxItemsQuery, IEnumerable<BoxItem>> {

        private readonly ApplicationContext context;

        public GetBoxItemsHandler(ApplicationContext db_context){
            context = db_context;
        }

        public async Task<IEnumerable<BoxItem>> Handle(GetBoxItemsQuery request, CancellationToken cancellationToken) {
            return await context.BoxItems.Include(bs => bs.Item).Where((b => b.BoxId == request.BoxId)).ToListAsync();
        }

    }
}