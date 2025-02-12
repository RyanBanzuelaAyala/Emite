using Emite.Application.Repository.V1;
using Emite.Domain.Model.V1.CustomerVM;
using MediatR;

namespace Emite.Infrastructure.V1.CustomerService.Command.Update
{
    public class UpdateCmdHandler : IRequestHandler<UpdateCmd, Customer>
    {
        private readonly IGenericRepository<Customer> _repository;

        public UpdateCmdHandler(IGenericRepository<Customer> CustomerRepository)
        {
            _repository = CustomerRepository;
        }

        public async Task<Customer> Handle(UpdateCmd request, CancellationToken cancellationToken)
        {
            _repository.Update(request.entity);

            return request.entity;
        }
    }
}
