using Emite.Application.Repository.V1;
using Emite.Domain.Model.V1.AgentVM;
using Emite.Domain.Model.V1.CallVM;
using Emite.SignalR.Channel;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;

public class CallRoutingService : ICallRoutingService
{
    private readonly IGenericRepository<Call> _CallRepository;
    private ConcurrentDictionary<string, Agent> _agents;
    private ConcurrentQueue<Call> _callQueue;
    private readonly IHubContext<EmiteNotificationHub> _hubContext;


    public CallRoutingService(IGenericRepository<Call> CallRepository, IHubContext<EmiteNotificationHub> hubContext)
    {
        _CallRepository = CallRepository;
        _agents = new ConcurrentDictionary<string, Agent>();
        _callQueue = new ConcurrentQueue<Call>();
        _hubContext = hubContext;
    }

    public void AddIncomingCall(Call call)
    {
        _callQueue.Enqueue(call);

        AssignCallToAgent();
    }

    private void AssignCallToAgent()
    {
        if (_callQueue.TryPeek(out Call call))
        {
            var availableAgent = _agents.Values.FirstOrDefault(agent => agent.Status == AgentStatus.Available);

            if (availableAgent != null)
            {
                _callQueue.TryDequeue(out call);
                availableAgent.Status = AgentStatus.Busy;
                call.AgentId = availableAgent.Id;
                call.Status = CallStatus.InProgress;
                _CallRepository.Create(call);

                NotifyAgent(availableAgent, call);
            }
        }
    }

    private async Task NotifyAgent(Agent agent, Call call)
    {
        string connectionId = agent.ConnectionId;

        if (!string.IsNullOrEmpty(connectionId))
        {
            await _hubContext.Clients.Client(connectionId).SendAsync("CallNotification", "A new call has been created.");
        }

    }

    public async Task<IEnumerable<Call>> SearchCallsAsync(CallStatus status, DateTime? startDate, DateTime? endDate, int agentId)
    {
        var query = (await _CallRepository.GetAllAsync()).AsQueryable();

        if (status != null)
        {
            query = query.Where(c => c.Status == status);
        }
        if (startDate.HasValue)
        {
            query = query.Where(c => c.StartTime >= startDate.Value);
        }
        if (endDate.HasValue)
        {
            query = query.Where(c => c.EndTime <= endDate.Value);
        }
        if (agentId != 0)
        {
            query = query.Where(c => c.AgentId == agentId);
        }

        return await query.ToListAsync();
    }
}
