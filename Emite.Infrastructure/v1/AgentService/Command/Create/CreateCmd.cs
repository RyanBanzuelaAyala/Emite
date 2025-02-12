using Emite.Domain.Model.V1.AgentVM;
using MediatR;

namespace Emite.Infrastructure.V1.AgentService.Command.Create
{
    public class CreateCmd : IRequest<Agent>
    {
        public Agent entity { get; set; }
    }
}