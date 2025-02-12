using Emite.Domain.Model.V1.AgentVM;
using MediatR;

namespace Emite.Infrastructure.V1.AgentService.Query.ListBy
{
    public class ListByQry : IRequest<IEnumerable<Agent>>
    {
        public int Id { get; set; }
    }
}