using Emite.Application.Repository.V1;
using Emite.Domain.Model.V1.CallVM;
using MediatR;

namespace Emite.Infrastructure.V1.CallService.Query.ListAll
{
    public class ListAllQryHandler : IRequestHandler<ListAllQry, IEnumerable<Call>>
    {
        private readonly IGenericRepository<Call> _repository;

        public ListAllQryHandler(IGenericRepository<Call> CallRepository)
        {
            _repository = CallRepository;
        }

        public async Task<IEnumerable<Call>> Handle(ListAllQry request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
