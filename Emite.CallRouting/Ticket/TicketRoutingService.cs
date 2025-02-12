using Emite.Application.Repository.V1;
using Emite.Domain.Model.V1.AgentVM;
using Emite.Domain.Model.V1.TicketVM;
using Emite.SignalR.Channel;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

public class TicketRoutingService : ITicketRoutingService
{
    private readonly IGenericRepository<Ticket> _ticketRepository;
    private ConcurrentDictionary<string, Agent> _agents;
    private ConcurrentQueue<Ticket> _ticketQueue;
    private readonly IHubContext<EmiteNotificationHub> _hubContext;

    public TicketRoutingService(IGenericRepository<Ticket> ticketRepository, IHubContext<EmiteNotificationHub> hubContext)
    {
        _ticketRepository = ticketRepository;
        _agents = new ConcurrentDictionary<string, Agent>();
        _ticketQueue = new ConcurrentQueue<Ticket>();
        _hubContext = hubContext;
    }

    public void AddIncomingTicket(Ticket ticket)
    {
        _ticketQueue.Enqueue(ticket);
        AssignTicketToAgent();
    }

    private async void AssignTicketToAgent()
    {
        if (_ticketQueue.TryPeek(out Ticket ticket))
        {
            var availableAgent = _agents.Values.FirstOrDefault(agent => agent.Status == AgentStatus.Available);

            if (availableAgent != null)
            {
                _ticketQueue.TryDequeue(out ticket);
                availableAgent.Status = AgentStatus.Busy;
                ticket.AgentId = availableAgent.Id;
                ticket.Status = TicketStatus.InProgress;
                _ticketRepository.Create(ticket);

                await NotifyAgent(availableAgent, ticket);
            }
        }
    }

    private async Task NotifyAgent(Agent agent, Ticket ticket)
    {
        string connectionId = agent.ConnectionId;

        if (!string.IsNullOrEmpty(connectionId))
        {
            await _hubContext.Clients.Client(connectionId).SendAsync("TicketNotification", $"A new ticket (ID: {ticket.Id}) has been assigned to you.");
        }
    }

    public async Task<IEnumerable<Ticket>> SearchTicketsAsync(TicketStatus status, int agentId)
    {
        var query = (await _ticketRepository.GetAllAsync()).AsQueryable();

        if (status != null)
        {
            query = query.Where(t => t.Status == status);
        }
        if (agentId != 0)
        {
            query = query.Where(t => t.AgentId == agentId);
        }

        return await query.ToListAsync();
    }
}
