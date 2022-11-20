using Microsoft.AspNetCore.SignalR;

namespace ProductsDemo.Api.Hubs
{
    public class NotificationHub: Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("createproduct", message);
        }
    }
}
