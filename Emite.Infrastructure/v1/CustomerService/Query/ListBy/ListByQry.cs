using Emite.Domain.Model.V1.CustomerVM;
using MediatR;

namespace Emite.Infrastructure.V1.CustomerService.Query.ListBy
{
    public class ListByQry : IRequest<IEnumerable<Customer>>
    {
        public int Id { get; set; }
    }
}