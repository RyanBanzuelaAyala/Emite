using Emite.Application.Repository.V1;
using Emite.Domain.Model.V1.TicketVM;
using MediatR;

namespace Emite.Infrastructure.V1.TicketService.Command.Delete
{
    public class DeleteCmdHandler : IRequestHandler<DeleteCmd, Ticket>
    {
        private readonly IGenericRepository<Ticket> _repository;

        public DeleteCmdHandler(IGenericRepository<Ticket> TicketRepository)
        {
            _repository = TicketRepository;
        }

        public async Task<Ticket> Handle(DeleteCmd request, CancellationToken cancellationToken)
        {
            var Ticket = _repository.GetBy(request.Id);

            if (Ticket is null)
            {
                throw new Exception("Ticket not found");
            }

            Ticket.Id = 0;

            _repository.Update(Ticket);

            return Ticket;
        }
    }
}
