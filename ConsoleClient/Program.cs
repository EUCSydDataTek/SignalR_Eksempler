using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.VisualBasic;
// See https://aka.ms/new-console-template for more information

HubConnection connection = new HubConnectionBuilder()
                               .WithUrl("https://localhost:7038/Notify")
                               .Build();

connection.On<string>("RecieveNotification", msg =>
{   
    Console.WriteLine($"Notification: {msg} \a");
});

connection.On<string, string>("RecieveNotificationFromGroup", (group, msg) =>
{
    Console.WriteLine($"{group}: {msg} \a");
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

        case ConsoleKey.M:
            SendMessage(connection);
            break;

        default:
            break;
    }


}
while (!Exit);

await connection.StopAsync();

async Task SendMessage(HubConnection connection)
{
    Console.Write("\nGroup:");
    string Group = Console.ReadLine();
    Console.Write("\nMessage:");
    string msg = Console.ReadLine();

    if (string.IsNullOrEmpty(Group))
    {
        await connection.SendAsync("SendGlobalNotification", msg);
        return;
    }

    await connection.SendAsync("SendToGroup",Group,msg);

}

async Task SubScribe(HubConnection connection)
{
    Console.Write("\n Subscibe to:");
    string group = Console.ReadLine();
    await connection.SendAsync("SubScribeTo", group);
}
 