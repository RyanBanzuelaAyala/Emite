using Emite.Domain.Model.V1.CustomerVM;
using MediatR;

namespace Emite.Infrastructure.V1.CustomerService.Query.ListAll
{
    public class ListAllQry : IRequest<IEnumerable<Customer>>
    {
    }
}