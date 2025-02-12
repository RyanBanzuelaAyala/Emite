using Emite.Domain.Model.V1.CallVM;
using MediatR;

namespace Emite.Infrastructure.V1.CallService.Query.ListBy
{
    public class ListByQry : IRequest<IEnumerable<Call>>
    {
        public int Id { get; set; }
    }
}