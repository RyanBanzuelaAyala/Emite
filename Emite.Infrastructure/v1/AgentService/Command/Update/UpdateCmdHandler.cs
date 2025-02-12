using Emite.Application.Repository.V1;
using Emite.Domain.Model.V1.AgentVM;
using MediatR;

namespace Emite.Infrastructure.V1.AgentService.Command.Update
{
    public class UpdateCmdHandler : IRequestHandler<UpdateCmd, Agent>
    {
        private readonly IGenericRepository<Agent> _repository;

        public UpdateCmdHandler(IGenericRepository<Agent> AgentRepository)
        {
            _repository = AgentRepository;
        }

        public async Task<Agent> Handle(UpdateCmd request, CancellationToken cancellationToken)
        {
            _repository.Update(request.entity);

            return request.entity;
        }
    }
}
