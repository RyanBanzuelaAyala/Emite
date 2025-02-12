using Emite.Application.Repository.V1;
using Emite.Domain.Model.V1.TicketVM;
using MediatR;

namespace Emite.Infrastructure.V1.TicketService.Command.Update
{
    public class UpdateCmdHandler : IRequestHandler<UpdateCmd, Ticket>
    {
        private readonly IGenericRepository<Ticket> _repository;

        public UpdateCmdHandler(IGenericRepository<Ticket> TicketRepository)
        {
            _repository = TicketRepository;
        }

        public async Task<Ticket> Handle(UpdateCmd request, CancellationToken cancellationToken)
        {
            _repository.Update(request.entity);

            return request.entity;
        }
    }
}
