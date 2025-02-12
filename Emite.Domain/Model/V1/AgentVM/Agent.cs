namespace Emite.Domain.Model.V1.AgentVM
{
    public class Agent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneExtension { get; set; }
        public AgentStatus Status { get; set; }
        public string ConnectionId { get; set; }
    }

    public enum AgentStatus
    {
        Available,
        Busy,
        Offline
    }

}
