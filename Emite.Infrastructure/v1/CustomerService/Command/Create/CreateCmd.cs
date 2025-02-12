using Emite.Domain.Model.V1.CustomerVM;
using MediatR;

namespace Emite.Infrastructure.V1.CustomerService.Command.Create
{
    public class CreateCmd : IRequest<Customer>
    {
        public Customer entity { get; set; }
    }
}