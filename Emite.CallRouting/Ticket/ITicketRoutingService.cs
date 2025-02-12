using Emite.Domain.Model.V1.TicketVM;

public interface ITicketRoutingService
{
    void AddIncomingTicket(Ticket ticket);
    Task<IEnumerable<Ticket>> SearchTicketsAsync(TicketStatus status, int agentId);
}
