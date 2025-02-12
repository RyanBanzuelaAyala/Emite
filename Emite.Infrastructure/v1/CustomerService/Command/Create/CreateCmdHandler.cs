using Emite.Application.Repository.V1;
using Emite.Domain.Model.V1.CustomerVM;
using MediatR;

namespace Emite.Infrastructure.V1.CustomerService.Command.Create
{
    public class CreateCmdHandler : IRequestHandler<CreateCmd, Customer>
    {
        private readonly IGenericRepository<Customer> _CustomerRepository;

        public CreateCmdHandler(IGenericRepository<Customer> CustomerRepository)
        {
            _CustomerRepository = CustomerRepository;
        }

        public async Task<Customer> Handle(CreateCmd request, CancellationToken cancellationToken)
        {

            _CustomerRepository.Create(request.entity);

            return request.entity;

        }
    }
}
