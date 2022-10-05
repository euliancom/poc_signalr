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
    }
    public async Task<bool> Handle(string message)
    {
        await HubConnection.StartAsync();
        await HubConnection.InvokeAsync("SendMessage", "API", message);
        await HubConnection.StopAsync();
        return true;
    }
}
