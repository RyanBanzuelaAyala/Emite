using Emite.Domain.Model.V1.TicketVM;
using MediatR;

namespace Emite.Infrastructure.V1.TicketService.Command.Update
{
    public class UpdateCmd : IRequest<Ticket>
    {
        public Ticket entity { get; set; }
    }
}