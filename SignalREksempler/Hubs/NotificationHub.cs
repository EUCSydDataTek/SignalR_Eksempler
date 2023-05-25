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

        public async Task SubScribeTo(string name)
        {
            logger.LogInformation($"{Context.ConnectionId} Subscribed to {name}");
            await Groups.AddToGroupAsync(Context.ConnectionId,name);
        }

        public async Task SendToGroup(string name, string message)
        {
            logger.LogInformation($"{name} message: {message}");
           await Clients.Group(name).SendAsync("RecieveNotificationFromGroup",name,message);
        }
    }
}
