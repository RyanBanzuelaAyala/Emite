using Emite.Base;
using Emite.Domain.Model.V1.AgentVM;
using Emite.Infrastructure.V1.AgentService.Command.Create;
using Emite.Infrastructure.V1.AgentService.Command.Delete;
using Emite.Infrastructure.V1.AgentService.Command.Update;
using Emite.Infrastructure.V1.AgentService.Command.UpdateStatus;
using Emite.Infrastructure.V1.AgentService.Query.ListAll;
using Emite.Infrastructure.V1.AgentService.Query.ListBy;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Emite.Api.Controllers
{
    public class AgentsController : EmiteController
    {
        public AgentsController(IMediator mediator, IMemoryCache cache) : base(mediator, cache)
        {
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost, Route("Create")]
        public async Task<ActionResult<Agent>> Create([FromBody] Agent entity)
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
        public async Task<ActionResult<Agent>> Update([FromBody] Agent entity)
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
        [HttpPut, Route("Update/{Id}/{Status}")]
        public async Task<ActionResult<Agent>> Update(int Id, AgentStatus Status)
        {
            try
            {
                return await _mediator.Send(new UpdateStatusCmd
                {
                    Id = Id,
                    Status = Status
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
        public async Task<ActionResult<Agent>> Delete([FromBody] int Id)
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
        public async Task<IEnumerable<Agent>> ListBy(int Id)
        {
            try
            {
                if (!_cache.TryGetValue($"ListBy_{Id}", out IEnumerable<Agent> agents))
                {
                    agents = await _mediator.Send(new ListByQry { Id = Id });

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                    _cache.Set($"ListBy_{Id}", agents, cacheEntryOptions);
                }

                return agents;
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
        public async Task<IEnumerable<Agent>> ListAll()
        {
            try
            {
                if (!_cache.TryGetValue("ListAll", out IEnumerable<Agent> agents))
                {
                    agents = await _mediator.Send(new ListAllQry());

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromMinutes(5));

                    _cache.Set("ListAll", agents, cacheEntryOptions);
                }

                return agents;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}