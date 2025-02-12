using Emite.Application.Repository.V1;
using Emite.Domain.Model.V1.CallVM;
using MediatR;

namespace Emite.Infrastructure.V1.CallService.Command.Update
{
    public class UpdateCmdHandler : IRequestHandler<UpdateCmd, Call>
    {
        private readonly IGenericRepository<Call> _repository;

        public UpdateCmdHandler(IGenericRepository<Call> CallRepository)
        {
            _repository = CallRepository;
        }

        public async Task<Call> Handle(UpdateCmd request, CancellationToken cancellationToken)
        {
            _repository.Update(request.entity);

            return request.entity;
        }
    }
}
