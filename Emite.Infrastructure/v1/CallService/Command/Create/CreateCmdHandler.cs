using Emite.Application.Repository.V1;
using Emite.Domain.Model.V1.CallVM;
using Emite.SignalR.Channel;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Emite.Infrastructure.V1.CallService.Command.Create
{
    public class CreateCmdHandler : IRequestHandler<CreateCmd, Call>
    {
        private readonly IGenericRepository<Call> _CallRepository;
        private readonly IHubContext<EmiteNotificationHub> _hubContext;

        public CreateCmdHandler(IGenericRepository<Call> CallRepository, IHubContext<EmiteNotificationHub> hubContext)
        {
            _CallRepository = CallRepository;
            _hubContext = hubContext;
        }

        public async Task<Call> Handle(CreateCmd request, CancellationToken cancellationToken)
        {

            _CallRepository.Create(request.entity);

            string connectionId = request.entity.ConnectionId;

            if (!string.IsNullOrEmpty(connectionId))
            {
                await _hubContext.Clients.Client(connectionId).SendAsync("CallNotification", "A new call has been created.");
            }

            return request.entity;

        }
    }
}
