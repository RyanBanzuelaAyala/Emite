using Emite.Base;
using Emite.Domain.Model.V1.CustomerVM;
using Emite.Infrastructure.V1.CustomerService.Command.Create;
using Emite.Infrastructure.V1.CustomerService.Command.Delete;
using Emite.Infrastructure.V1.CustomerService.Command.Update;
using Emite.Infrastructure.V1.CustomerService.Query.ListAll;
using Emite.Infrastructure.V1.CustomerService.Query.ListBy;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Emite.Api.Controllers
{
    public class CustomersController : EmiteController
    {
        public CustomersController(IMediator mediator, IMemoryCache cache) : base(mediator, cache)
        {
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [HttpPost, Route("Create")]
        public async Task<ActionResult<Customer>> Create([FromBody] Customer entity)
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
        public async Task<ActionResult<Customer>> Update([FromBody] Customer entity)
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
        public async Task<ActionResult<Customer>> Delete([FromBody] int Id)
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
        public async Task<IEnumerable<Customer>> ListBy(int Id)
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
        public async Task<IEnumerable<Customer>> ListAll()
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