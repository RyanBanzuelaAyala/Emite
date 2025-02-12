using Emite.Domain.Model.V1.TicketVM;
using MediatR;

namespace Emite.Infrastructure.V1.TicketService.Command.Assign
{
    public class AssignCmd : IRequest<Ticket>
    {
        public Ticket entity { get; set; }

        public int agentId { get; set; }
    }
}