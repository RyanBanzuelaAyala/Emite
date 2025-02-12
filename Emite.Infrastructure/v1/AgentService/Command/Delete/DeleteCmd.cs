using Emite.Domain.Model.V1.AgentVM;
using MediatR;

namespace Emite.Infrastructure.V1.AgentService.Command.Delete
{
    public class DeleteCmd : IRequest<Agent>
    {
        public int Id { get; set; }
    }
}