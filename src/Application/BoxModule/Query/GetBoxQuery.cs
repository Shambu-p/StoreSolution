using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using StoreBackendClean.Infrastructure.Persistance;
using StoreBackendClean.Domain.Entity;

namespace StoreBackendClean.Application.BoxModule.Query
{
    public class GetBoxQuery : IRequest<Box> {
        
        public uint Id {get; init;}

        public GetBoxQuery(uint id){
            this.Id = id;
        }

    }

    public class GetBoxHandler : IRequestHandler<GetBoxQuery, Box> {

        private readonly ApplicationContext context;

        public GetBoxHandler(ApplicationContext db_context){
            context = db_context;
        }

        public async Task<Box> Handle(GetBoxQuery request, CancellationToken cancellationToken) {
            
            Box bx = context.Boxes
                    .Include(b => b.Store)
                    .Include(b => b.User)
                    .Where(b => b.Id == request.Id).FirstOrDefault();
                    
            if(bx == null){
                throw new Exception("Box not found!");
            }

            return bx;

        }

    }
}