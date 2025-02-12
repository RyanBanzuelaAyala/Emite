using Emite.Application.Repository.V1;
using Emite.Domain.Model.V1.AgentVM;
using MediatR;

namespace Emite.Infrastructure.V1.AgentService.Command.Delete
{
    public class DeleteCmdHandler : IRequestHandler<DeleteCmd, Agent>
    {
        private readonly IGenericRepository<Agent> _repository;

        public DeleteCmdHandler(IGenericRepository<Agent> AgentRepository)
        {
            _repository = AgentRepository;
        }

        public async Task<Agent> Handle(DeleteCmd request, CancellationToken cancellationToken)
        {
            var Agent = _repository.GetBy(request.Id);

            if (Agent is null)
            {
                throw new Exception("Agent not found");
            }

            Agent.Id = 0;

            _repository.Update(Agent);

            return Agent;
        }
    }
}
