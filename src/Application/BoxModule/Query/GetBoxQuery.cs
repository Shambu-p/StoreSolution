using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using StoreSolution.Application.common.Interfaces;
using StoreSolution.Domain.Entity;

namespace StoreSolution.Application.BoxModule.Query
{
    public class GetBoxQuery : IRequest<Box> {
        
        public uint Id {get; init;}

        public GetBoxQuery(uint id){
            this.Id = id;
        }

    }

    public class GetBoxHandler : IRequestHandler<GetBoxQuery, Box> {

        private readonly IDBContext context;

        public GetBoxHandler(IDBContext db_context){
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