using Emite.Application.Repository.V1;
using Emite.Domain.Model.V1.TicketVM;
using MediatR;

namespace Emite.Infrastructure.V1.TicketService.Query.ListBy
{
    public class ListByQryHandler : IRequestHandler<ListByQry, IEnumerable<Ticket>>
    {
        private readonly IGenericRepository<Ticket> _repository;

        public ListByQryHandler(IGenericRepository<Ticket> TicketRepository)
        {
            _repository = TicketRepository;
        }

        public async Task<IEnumerable<Ticket>> Handle(ListByQry request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync(s => s.Id == request.Id);
        }
    }
}
