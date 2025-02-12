using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Emite.Base
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class EmiteController : ControllerBase
    {
        public readonly IMediator _mediator;

        public readonly IMemoryCache _cache;

        public EmiteController(IMediator mediator, IMemoryCache cache)
        {
            _mediator = mediator;
            _cache = cache;
        }
    }
}
