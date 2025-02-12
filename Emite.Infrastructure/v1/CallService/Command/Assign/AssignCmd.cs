using Emite.Domain.Model.V1.CallVM;
using MediatR;

namespace Emite.Infrastructure.V1.CallService.Command.Assign
{
    public class AssignCmd : IRequest<Call>
    {
        public Call entity { get; set; }
        public int agentId { get; set; }
    }
}