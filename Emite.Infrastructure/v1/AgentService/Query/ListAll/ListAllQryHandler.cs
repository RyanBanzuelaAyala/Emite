using Emite.Application.Repository.V1;
using Emite.Domain.Model.V1.AgentVM;
using MediatR;

namespace Emite.Infrastructure.V1.AgentService.Query.ListAll
{
    public class ListAllQryHandler : IRequestHandler<ListAllQry, IEnumerable<Agent>>
    {
        private readonly IGenericRepository<Agent> _repository;

        public ListAllQryHandler(IGenericRepository<Agent> AgentRepository)
        {
            _repository = AgentRepository;
        }

        public async Task<IEnumerable<Agent>> Handle(ListAllQry request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
