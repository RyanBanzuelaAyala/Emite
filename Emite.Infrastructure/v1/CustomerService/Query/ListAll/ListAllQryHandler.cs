using Emite.Application.Repository.V1;
using Emite.Domain.Model.V1.CustomerVM;
using MediatR;

namespace Emite.Infrastructure.V1.CustomerService.Query.ListAll
{
    public class ListAllQryHandler : IRequestHandler<ListAllQry, IEnumerable<Customer>>
    {
        private readonly IGenericRepository<Customer> _repository;

        public ListAllQryHandler(IGenericRepository<Customer> CustomerRepository)
        {
            _repository = CustomerRepository;
        }

        public async Task<IEnumerable<Customer>> Handle(ListAllQry request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
