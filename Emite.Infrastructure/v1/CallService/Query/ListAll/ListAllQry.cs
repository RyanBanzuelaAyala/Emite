using Emite.Domain.Model.V1.CallVM;
using MediatR;

namespace Emite.Infrastructure.V1.CallService.Query.ListAll
{
    public class ListAllQry : IRequest<IEnumerable<Call>>
    {
    }
}