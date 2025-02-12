using Emite.SignalR.Channel.Utilities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;

namespace Emite.SignalR.Channel;

public class EmiteNotificationHub : Hub
{
    private readonly IConfiguration _config;
    public EmiteNotificationHub(IConfiguration config)
    {
        _config = config;
    }

    private readonly static UCMapping<string> _connections = new UCMapping<string>();

    public async Task SendNotificationCall(string connectionId, string callNotification)
    {
        try
        {
            await Clients.Client(connectionId).SendAsync("CallNotification", callNotification);

        }
        catch (Exception)
        {
            throw;
        }

    }

    public async Task SendNotificationTicket(string connectionId, string ticketNotification)
    {
        try
        {
            await Clients.Client(connectionId).SendAsync("TicketNotification", ticketNotification);

        }
        catch (Exception)
        {
            throw;
        }

    }


    #region User Connection

    public string GetConnectionId()
    {
        return Context.ConnectionId;
    }

    public override async Task OnConnectedAsync()
    {
        _connections.Add(Context.ConnectionId, Context.ConnectionId);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        _connections.Remove(Context.ConnectionId, Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }

    public bool IsConnectionExist(string ConnectionId)
    {
        return _connections.IsExist(ConnectionId);
    }

    #endregion
}
