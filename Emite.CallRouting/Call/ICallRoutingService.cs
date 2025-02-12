using Emite.Domain.Model.V1.CallVM;

public interface ICallRoutingService
{
    void AddIncomingCall(Call call);
    Task<IEnumerable<Call>> SearchCallsAsync(CallStatus status, DateTime? startDate, DateTime? endDate, int agentId);
}
