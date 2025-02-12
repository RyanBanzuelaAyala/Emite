using Emite.Application.Repository.V1;
using Emite.Domain.Model.V1.CustomerVM;
using MediatR;

namespace Emite.Infrastructure.V1.CustomerService.Command.Delete
{
    public class DeleteCmdHandler : IRequestHandler<DeleteCmd, Customer>
    {
        private readonly IGenericRepository<Customer> _repository;

        public DeleteCmdHandler(IGenericRepository<Customer> CustomerRepository)
        {
            _repository = CustomerRepository;
        }

        public async Task<Customer> Handle(DeleteCmd request, CancellationToken cancellationToken)
        {
            var Customer = _repository.GetBy(request.Id);

            if (Customer is null)
            {
                throw new Exception("Customer not found");
            }

            Customer.Id = 0;

            _repository.Update(Customer);

            return Customer;
        }
    }
}
