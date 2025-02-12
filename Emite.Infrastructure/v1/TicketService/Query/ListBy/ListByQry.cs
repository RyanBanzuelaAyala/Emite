using Emite.Domain.Model.V1.TicketVM;
using MediatR;

namespace Emite.Infrastructure.V1.TicketService.Query.ListBy
{
    public class ListByQry : IRequest<IEnumerable<Ticket>>
    {
        public int Id { get; set; }
    }
}