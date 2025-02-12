using Emite.Domain.Model.V1.TicketVM;
using MediatR;

namespace Emite.Infrastructure.V1.TicketService.Query.ListAll
{
    public class ListAllQry : IRequest<IEnumerable<Ticket>>
    {
    }
}