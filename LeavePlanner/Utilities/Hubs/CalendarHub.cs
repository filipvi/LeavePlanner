using Microsoft.AspNetCore.SignalR;

namespace LeavePlanner.Utilities.Hubs;

public class CalendarHub : Hub
{
    public async Task RefreshEvents()
    {
        await Clients.All.SendAsync("RefreshEvents");
    }
}