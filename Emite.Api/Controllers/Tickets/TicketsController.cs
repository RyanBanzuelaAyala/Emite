using Emite.Base;
using Emite.Domain.Model.V1.TicketVM;
using Emite.Infrastructure.V1.TicketService.Command.Create;
using Emite.Infrastructure.V1.TicketService.Command.Delete;
using Emite.Infrastructure.V1.TicketService.Command.Update;
using Emite.Infrastructure.V1.TicketService.Query.ListAll;
using Emite.Infrastructure.V1.TicketService.Query.ListBy;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Emite.Api.Controllers
{
    public class TicketsController : EmiteController
    {
        public TicketsController(IMediator mediator, IMemoryCache cache) : base(mediator, cache)
        {
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost, Route("Create")]
        public async Task<ActionResult<Ticket>> Create([FromBody] Ticket entity)
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
        public async Task<ActionResult<Ticket>> Update([FromBody] Ticket entity)
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
        public async Task<ActionResult<Ticket>> Delete([FromBody] int Id)
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
        public async Task<IEnumerable<Ticket>> ListBy(int Id)
        {
            try
            {
                return await _mediator.Send(new ListByQry
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
        [HttpGet, Route("ListAll"), AllowAnonymous]
        public async Task<IEnumerable<Ticket>> ListAll()
        {
            try
            {
                return await _mediator.Send(new ListAllQry
                {
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}