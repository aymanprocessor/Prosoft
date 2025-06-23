using Microsoft.AspNetCore.SignalR;

namespace ProSoft.UI.Hubs
{

    public class ConnectionHub : Hub
    {
        public async Task Ping()
        {
            await Clients.Caller.SendAsync("ReceiveStatus", $"متصل");
        }
    }
}
