using Emite.Domain.Model.V1.AgentVM;
using MediatR;

namespace Emite.Infrastructure.V1.AgentService.Query.ListAll
{
    public class ListAllQry : IRequest<IEnumerable<Agent>>
    {
    }
}