using Emite.Domain.Model.V1.CustomerVM;
using MediatR;

namespace Emite.Infrastructure.V1.CustomerService.Command.Update
{
    public class UpdateCmd : IRequest<Customer>
    {
        public Customer entity { get; set; }
    }
}