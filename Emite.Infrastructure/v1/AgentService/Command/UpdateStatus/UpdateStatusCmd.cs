using Emite.Domain.Model.V1.AgentVM;
using MediatR;

namespace Emite.Infrastructure.V1.AgentService.Command.UpdateStatus
{
    public class UpdateStatusCmd : IRequest<Agent>
    {
        public int Id { get; set; }
        public AgentStatus Status { get; set; }
    }
}