namespace Emite.Domain.Model.V1.TicketVM
{
    public class Ticket
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public int? AgentId { get; set; }
        public TicketStatus Status { get; set; }
        public Priority Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Description { get; set; }
        public string Resolution { get; set; }
    }
    public enum TicketStatus
    {
        Open,
        InProgress,
        Resolved,
        Closed
    }

    public enum Priority
    {
        Low,
        Medium,
        High,
        Urgent
    }


}
