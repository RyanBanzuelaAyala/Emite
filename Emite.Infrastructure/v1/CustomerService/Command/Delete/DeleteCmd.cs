using Emite.Domain.Model.V1.CustomerVM;
using MediatR;

namespace Emite.Infrastructure.V1.CustomerService.Command.Delete
{
    public class DeleteCmd : IRequest<Customer>
    {
        public int Id { get; set; }
    }
}