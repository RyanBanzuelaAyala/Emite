using Emite.Application.Repository.V1;
using Emite.Domain.Model.V1.AgentVM;
using MediatR;

namespace Emite.Infrastructure.V1.AgentService.Command.Create
{
    public class CreateCmdHandler : IRequestHandler<CreateCmd, Agent>
    {
        private readonly IGenericRepository<Agent> _AgentRepository;

        public CreateCmdHandler(IGenericRepository<Agent> AgentRepository)
        {
            _AgentRepository = AgentRepository;
        }

        public async Task<Agent> Handle(CreateCmd request, CancellationToken cancellationToken)
        {

            _AgentRepository.Create(request.entity);

            return request.entity;

        }
    }
}
