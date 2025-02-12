using Emite.Application.Repository.V1;
using Emite.Domain.Model.V1.TicketVM;
using MediatR;

namespace Emite.Infrastructure.V1.TicketService.Command.Assign
{
    public class AssignCmdHandler : IRequestHandler<AssignCmd, Ticket>
    {
        private readonly IGenericRepository<Ticket> _repository;

        public AssignCmdHandler(IGenericRepository<Ticket> TicketRepository)
        {
            _repository = TicketRepository;
        }

        public async Task<Ticket> Handle(AssignCmd request, CancellationToken cancellationToken)
        {
            var Ticket = _repository.GetBy(request.entity.Id);

            if (Ticket is null)
            {
                throw new Exception("Ticket not found");
            }

            Ticket.AgentId = request.agentId;

            _repository.Update(request.entity);

            return request.entity;
        }
    }
}
