using Microsoft.AspNetCore.SignalR.Client;
// See https://aka.ms/new-console-template for more information

HubConnection connection = new HubConnectionBuilder()
                               .WithUrl("https://localhost:7038/Notify")
                               .Build();

connection.On<string>("RecieveNotification", msg =>
{   
    Console.WriteLine($"Notification: {msg} \a");
});

await connection.StartAsync();

while (true)
{
    string msg = Console.ReadLine();
    await connection.SendAsync("SendGlobalNotification", msg);
}

