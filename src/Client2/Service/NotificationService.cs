using Client2.Extensions;
using Microsoft.AspNetCore.SignalR.Client;

namespace Client2.Service;

public class NotificationService
{
    private readonly HubConnection hubConnection;
    public event Action<string>? OnNotificationReceived;

    public NotificationService()
    {
        hubConnection = new HubConnectionBuilder()
            .WithUrl(Constants.BaseAddress + "notificationHub")
            .Build();

        hubConnection.On<string>("ReceiveNotification", (message) =>
        {
            OnNotificationReceived?.Invoke(message);
        });
    }

    public async Task StartAsync()
    {
        await hubConnection.StartAsync();
    }

    public async Task SendNotification(string message)
    {
        await hubConnection.InvokeAsync("SendNotification", message);
    }
}