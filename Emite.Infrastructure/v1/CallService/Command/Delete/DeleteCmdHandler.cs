using Emite.Application.Repository.V1;
using Emite.Domain.Model.V1.CallVM;
using MediatR;

namespace Emite.Infrastructure.V1.CallService.Command.Delete
{
    public class DeleteCmdHandler : IRequestHandler<DeleteCmd, Call>
    {
        private readonly IGenericRepository<Call> _repository;

        public DeleteCmdHandler(IGenericRepository<Call> CallRepository)
        {
            _repository = CallRepository;
        }

        public async Task<Call> Handle(DeleteCmd request, CancellationToken cancellationToken)
        {
            var Call = _repository.GetBy(request.Id);

            if (Call is null)
            {
                throw new Exception("Call not found");
            }

            Call.Id = 0;

            _repository.Update(Call);

            return Call;
        }
    }
}
