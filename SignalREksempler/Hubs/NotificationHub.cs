using Microsoft.AspNetCore.SignalR;

namespace SignalREksempler.Hubs
{
    public class NotificationHub(ILogger<NotificationHub> logger) : Hub
    {

        public override Task OnConnectedAsync()
        {
            logger.LogInformation($"Client {Context.ConnectionId} connected to hub");
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            if (exception != null)
            {
                logger.LogWarning(exception.Message);
            }

            logger.LogInformation($"Client {Context.ConnectionId} disconnected");

            return base.OnDisconnectedAsync(exception);
        }

        public async Task SendGlobalNotification(string message)
        {
            await Clients.Others.SendAsync("RecieveNotification",message);
            logger.LogInformation($"Global message: {message}");
        }
    }
}
