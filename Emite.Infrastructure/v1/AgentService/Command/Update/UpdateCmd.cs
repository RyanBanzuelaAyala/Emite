using Emite.Domain.Model.V1.AgentVM;
using MediatR;

namespace Emite.Infrastructure.V1.AgentService.Command.Update
{
    public class UpdateCmd : IRequest<Agent>
    {
        public Agent entity { get; set; }
    }
}