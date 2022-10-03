using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace StoreBackendClean.Controllers {

    [ApiController]
    [Route("api/[controller]")]
    public class ApiController : ControllerBase {

        private ISender _mediator = null!;

        protected ISender mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    }
}