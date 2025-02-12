using Emite.Application.Repository.V1;
using Emite.Domain.Model.V1.AgentVM;
using MediatR;

namespace Emite.Infrastructure.V1.AgentService.Query.ListBy
{
    public class ListByQryHandler : IRequestHandler<ListByQry, IEnumerable<Agent>>
    {
        private readonly IGenericRepository<Agent> _repository;

        public ListByQryHandler(IGenericRepository<Agent> AgentRepository)
        {
            _repository = AgentRepository;
        }

        public async Task<IEnumerable<Agent>> Handle(ListByQry request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(s => s.Id == request.Id);
        }
    }
}
