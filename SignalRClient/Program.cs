using System;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace SignalRClient
{
    class Program
    {
        static HubConnection hubConnection;
        static async Task Main(string[] args)
        {
            hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:6771/notification")
                .Build();

            hubConnection.On<string>("Send", message => Console.WriteLine($"Message from server: {message}"));

            await hubConnection.StartAsync();

            bool isExit = false;

            while (!isExit)
            {
                var message = Console.ReadLine();

                if (message != "exit")
                    await hubConnection.SendAsync("SendMessage", message);
                else
                    isExit = true;
            }

            Console.ReadLine();
        }
    }
}
