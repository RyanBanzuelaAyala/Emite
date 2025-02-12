using Emite.Application.Repository.V1;
using Emite.Domain.Model.V1.TicketVM;
using MediatR;

namespace Emite.Infrastructure.V1.TicketService.Query.ListAll
{
    public class ListAllQryHandler : IRequestHandler<ListAllQry, IEnumerable<Ticket>>
    {
        private readonly IGenericRepository<Ticket> _repository;

        public ListAllQryHandler(IGenericRepository<Ticket> TicketRepository)
        {
            _repository = TicketRepository;
        }

        public async Task<IEnumerable<Ticket>> Handle(ListAllQry request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
