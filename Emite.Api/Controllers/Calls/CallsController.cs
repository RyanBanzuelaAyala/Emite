using Emite.Base;
using Emite.Domain.Model.V1.CallVM;
using Emite.Infrastructure.V1.CallService.Command.Create;
using Emite.Infrastructure.V1.CallService.Command.Delete;
using Emite.Infrastructure.V1.CallService.Command.Update;
using Emite.Infrastructure.V1.CallService.Query.ListAll;
using Emite.Infrastructure.V1.CallService.Query.ListBy;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Emite.Api.Controllers
{
    public class CallsController : EmiteController
    {
        private readonly IMemoryCache _cache;

        public CallsController(IMediator mediator, IMemoryCache cache) : base(mediator, cache)
        {
            _cache = cache;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost, Route("Create")]
        public async Task<ActionResult<Call>> Create([FromBody] Call entity)
        {
            try
            {
                var result = await _mediator.Send(new CreateCmd
                {
                    entity = entity
                });

                return result;

            }
            catch (Exception)
            {
                throw;
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPut, Route("Update")]
        public async Task<ActionResult<Call>> Update([FromBody] Call entity)
        {
            try
            {
                return await _mediator.Send(new UpdateCmd
                {
                    entity = entity
                });

            }
            catch (Exception)
            {
                throw;
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpDelete, Route("Delete")]
        public async Task<ActionResult<Call>> Delete([FromBody] int Id)
        {
            try
            {
                return await _mediator.Send(new DeleteCmd
                {
                    Id = Id
                });

            }
            catch (Exception)
            {
                throw;
            }

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpGet, Route("ListBy/{Id}"), AllowAnonymous]
        public async Task<IEnumerable<Call>> ListBy(int Id)
        {
            try
            {
                if (!_cache.TryGetValue($"ListBy_{Id}", out IEnumerable<Call> calls))
                {
                    calls = await _mediator.Send(new ListByQry
                    {
                        Id = Id
                    });

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                    _cache.Set($"ListBy_{Id}", calls, cacheEntryOptions);
                }

                return calls;
            }
            catch (Exception)
            {
                throw;
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpGet, Route("ListAll"), AllowAnonymous]
        public async Task<IEnumerable<Call>> ListAll()
        {
            try
            {
                if (!_cache.TryGetValue("ListAll", out IEnumerable<Call> calls))
                {
                    calls = await _mediator.Send(new ListAllQry
                    {
                    });

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                    _cache.Set("ListAll", calls, cacheEntryOptions);
                }

                return calls;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
