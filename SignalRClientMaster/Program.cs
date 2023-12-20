using Microsoft.AspNetCore.SignalR.Client;

internal class Program
{
    private static async Task Main(string[] args)
    {
        HubConnection connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7228/chatHub")
                .Build();

        var user = new UserModel()
        {
            ConnectionId = connection.ConnectionId,
            Usid = "00002639"
        };

        connection.StartAsync().ContinueWith(t =>
        {
            if (t.IsFaulted)
                Console.WriteLine(t.Exception.GetBaseException());
            else
            {
                connection.InvokeAsync("JoinHubList", connection.ConnectionId, user);
                Console.WriteLine("Connected to Hub");
            }
        }).Wait();
        connection.On<string, string>("GetMessage", (user, message) =>
        {
            Console.WriteLine(user + " " + message);
        });
        while (true)
        {

            Console.WriteLine("Göndermek istediğiniz mesajı yazınız..");
            var message = Console.ReadLine();

            await connection.InvokeAsync<string>("SendMessage", "00001864", message);

            //await Task.Factory.StartNew(() => { });
            Console.WriteLine("Çıkmak için q tuşa basın..");

            var key = Console.ReadLine();
            if (key == "q") break;
        }

        await connection.DisposeAsync().AsTask().ContinueWith(t =>
        {
            if (t.IsFaulted)
                Console.WriteLine(t.Exception.GetBaseException());
            else
                Console.WriteLine("Disconnected");
        });


    }
}

public class UserModel
{
    public string ConnectionId { get; set; }
    public string Usid { get; set; }
}