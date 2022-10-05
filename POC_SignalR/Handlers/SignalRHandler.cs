using Microsoft.AspNetCore.SignalR.Client;

namespace POC_SignalR.Handlers;

public class SignalRHandler
{
    public HubConnection HubConnection { get; set; }

    public SignalRHandler()
    {
        HubConnection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5188/chatHub")
            .Build();
        HubConnection.Closed += async (error) =>
        {
            await Task.Delay(new Random().Next(0, 5) * 1000);
            await HubConnection.StartAsync();
        };
    }

    public async 
    Task
Open()
    {
        await HubConnection.StartAsync();
    }

    public async Task<bool> Handle(string message)
    {
        await HubConnection.InvokeAsync("SendMessage", "API", message);
        return true;
    }

    public async void Stop()
    {
        await HubConnection.StopAsync();
    }
}
