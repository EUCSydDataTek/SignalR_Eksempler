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

bool Exit = false;

do
{
    var key = Console.ReadKey();
    switch (key.Key)
    {
        case ConsoleKey.X:
            Exit = true;
            break;

        case ConsoleKey.S:
            SubScribe(connection); 
            break;

        case ConsoleKey.G:
            GlobalMessage(connection);
            break;

        default:
            break;
    }


}
while (!Exit);

await connection.StopAsync();

async Task GlobalMessage(HubConnection connection)
{
    Console.Write("\nGlobal message:");
    string msg = Console.ReadLine();
    await connection.SendAsync("SendGlobalNotification", msg);
}

async Task SubScribe(HubConnection connection)
{
    Console.Write("\n Subscibe to:");
    string group = Console.ReadLine();
    await connection.SendAsync("SubScribeTo", group);
}
 