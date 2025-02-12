using Emite.Domain.Model.V1.TicketVM;
using MediatR;

namespace Emite.Infrastructure.V1.TicketService.Command.Delete
{
    public class DeleteCmd : IRequest<Ticket>
    {
        public int Id { get; set; }
    }
}