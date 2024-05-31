using Microsoft.AspNetCore.SignalR;

namespace Vouzamo.ScoreStream.WebApp.Hubs
{
    public class ScoreHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}