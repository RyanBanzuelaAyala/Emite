using Emite.Application.Repository.V1;
using Emite.Domain.Model.V1.AgentVM;
using MediatR;

namespace Emite.Infrastructure.V1.AgentService.Command.UpdateStatus
{
    public class UpdateStatusCmdHandler : IRequestHandler<UpdateStatusCmd, Agent>
    {
        private readonly IGenericRepository<Agent> _repository;

        public UpdateStatusCmdHandler(IGenericRepository<Agent> AgentRepository)
        {
            _repository = AgentRepository;
        }

        public async Task<Agent> Handle(UpdateStatusCmd request, CancellationToken cancellationToken)
        {
            var agent = _repository.GetBy(request.Id);

            agent.Status = request.Status;

            _repository.Update(agent);

            return agent;
        }
    }
}
