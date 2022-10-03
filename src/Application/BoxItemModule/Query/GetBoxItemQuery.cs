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
    public class GetBoxItemQuery : IRequest<BoxItem> {
        
        public uint BoxId {get; init;}
        public uint ItemId {get; init;}

        public GetBoxItemQuery(uint box_id, uint item_id){
            this.BoxId = box_id;
            this.ItemId = item_id;
        }
        
    }

    public class GetBoxItemHandler : IRequestHandler<GetBoxItemQuery, BoxItem> {

        private readonly ApplicationContext context;

        public GetBoxItemHandler(ApplicationContext db_context){
            context = db_context;
        }

        public async Task<BoxItem> Handle(GetBoxItemQuery request, CancellationToken cancellationToken) {
            return context.BoxItems
                .Include(b => b.Box)
                .Include(b => b.Item)
                .Where(b => b.BoxId == request.BoxId && b.ItemId == request.ItemId)
                .FirstOrDefault();
        }

    }
}