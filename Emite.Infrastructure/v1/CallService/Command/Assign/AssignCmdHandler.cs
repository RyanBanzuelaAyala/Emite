using Emite.Application.Repository.V1;
using Emite.Domain.Model.V1.CallVM;
using MediatR;

namespace Emite.Infrastructure.V1.CallService.Command.Assign
{
    public class AssignCmdHandler : IRequestHandler<AssignCmd, Call>
    {
        private readonly IGenericRepository<Call> _repository;

        public AssignCmdHandler(IGenericRepository<Call> CallRepository)
        {
            _repository = CallRepository;
        }

        public async Task<Call> Handle(AssignCmd request, CancellationToken cancellationToken)
        {
            var Call = _repository.GetBy(request.entity.Id);

            if (Call is null)
            {
                throw new Exception("Call not found");
            }

            Call.AgentId = request.agentId;

            _repository.Update(request.entity);

            return request.entity;
        }
    }
}
