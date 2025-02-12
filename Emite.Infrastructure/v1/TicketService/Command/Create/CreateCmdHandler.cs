using Emite.Application.Repository.V1;
using Emite.Domain.Model.V1.TicketVM;
using MediatR;

namespace Emite.Infrastructure.V1.TicketService.Command.Create
{
    public class CreateCmdHandler : IRequestHandler<CreateCmd, Ticket>
    {
        private readonly IGenericRepository<Ticket> _TicketRepository;

        public CreateCmdHandler(IGenericRepository<Ticket> TicketRepository)
        {
            _TicketRepository = TicketRepository;
        }

        public async Task<Ticket> Handle(CreateCmd request, CancellationToken cancellationToken)
        {

            _TicketRepository.Create(request.entity);

            return request.entity;

        }
    }
}
