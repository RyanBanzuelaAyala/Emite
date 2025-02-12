using Emite.Application.Repository.V1;
using Emite.Domain.Model.V1.CustomerVM;
using MediatR;

namespace Emite.Infrastructure.V1.CustomerService.Query.ListBy
{
    public class ListByQryHandler : IRequestHandler<ListByQry, IEnumerable<Customer>>
    {
        private readonly IGenericRepository<Customer> _repository;

        public ListByQryHandler(IGenericRepository<Customer> CustomerRepository)
        {
            _repository = CustomerRepository;
        }

        public async Task<IEnumerable<Customer>> Handle(ListByQry request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(s => s.Id == request.Id);
        }
    }
}
