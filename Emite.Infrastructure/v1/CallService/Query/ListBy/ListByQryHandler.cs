using Emite.Application.Repository.V1;
using Emite.Domain.Model.V1.CallVM;
using MediatR;

namespace Emite.Infrastructure.V1.CallService.Query.ListBy
{
    public class ListByQryHandler : IRequestHandler<ListByQry, IEnumerable<Call>>
    {
        private readonly IGenericRepository<Call> _repository;

        public ListByQryHandler(IGenericRepository<Call> CallRepository)
        {
            _repository = CallRepository;
        }

        public async Task<IEnumerable<Call>> Handle(ListByQry request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(s => s.Id == request.Id);
        }
    }
}
