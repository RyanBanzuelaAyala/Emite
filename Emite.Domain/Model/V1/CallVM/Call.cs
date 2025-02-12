namespace Emite.Domain.Model.V1.CallVM
{
    public class Call
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public int? AgentId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public CallStatus Status { get; set; }
        public string Notes { get; set; }

        public string ConnectionId { get; set; }
    }

    public enum CallStatus
    {
        Queued,
        InProgress,
        Completed,
        Dropped
    }
}
