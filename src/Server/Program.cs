using System.Net.Sockets;
using Microsoft.AspNetCore.SignalR;
using Microsoft.OpenApi.Models;
using Server.Hubs;

namespace Server;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSignalR();
        builder.Services.AddCors(options => options.AddPolicy("AllowAll", builder
            => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        app.MapHub<NotificationHub>("/notificationHub");

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("AllowAll");

        app.MapPost("/notify-client2", async (IHubContext<NotificationHub> hubContext, Notification notification) =>
        {
            await hubContext.Clients.All.SendAsync("ReceiveNotification", notification.Message);
            return Results.NoContent();
        })
        .Produces(StatusCodes.Status204NoContent)
        .WithOpenApi(options =>
        {
            options.Tags = [new OpenApiTag { Name = "Notifications" }];
            options.Summary = "Send a notification to all clients";
            options.Description = "Send a notification to all clients";

            return options;
        });

        app.MapPost("/notify-client", async (IHubContext<NotificationHub> hubContext, Notification notification) =>
        {
            await hubContext.Clients.All.SendAsync("ReceiveNotification", notification.Message, "Normal");
            return Results.NoContent();
        })
        .Produces(StatusCodes.Status204NoContent)
        .WithOpenApi(options =>
        {
            options.Tags = [new OpenApiTag { Name = "Notifications" }];
            options.Summary = "Send a notification to all clients";
            options.Description = "Send a notification to all clients";

            return options;
        });

        app.MapPost("/notify-client-info", async (IHubContext<NotificationHub> hubContext, Notification notification) =>
        {
            await hubContext.Clients.All.SendAsync("ReceiveNotification", notification.Message, "Info");
            return Results.NoContent();
        })
        .Produces(StatusCodes.Status204NoContent)
        .WithOpenApi(options =>
        {
            options.Tags = [new OpenApiTag { Name = "Notifications" }];
            options.Summary = "Send a notification to all clients";
            options.Description = "Send a notification to all clients";

            return options;
        });

        app.MapPost("/notify-client-success", async (IHubContext<NotificationHub> hubContext, Notification notification) =>
        {
            await hubContext.Clients.All.SendAsync("ReceiveNotification", notification.Message, "Success");
            return Results.NoContent();
        })
        .Produces(StatusCodes.Status204NoContent)
        .WithOpenApi(options =>
        {
            options.Tags = [new OpenApiTag { Name = "Notifications" }];
            options.Summary = "Send a notification to all clients";
            options.Description = "Send a notification to all clients";

            return options;
        });

        app.MapPost("/notify-client-warning", async (IHubContext<NotificationHub> hubContext, Notification notification) =>
        {
            await hubContext.Clients.All.SendAsync("ReceiveNotification", notification.Message, "Warning");
            return Results.NoContent();
        })
        .Produces(StatusCodes.Status204NoContent)
        .WithOpenApi(options =>
        {
            options.Tags = [new OpenApiTag { Name = "Notifications" }];
            options.Summary = "Send a notification to all clients";
            options.Description = "Send a notification to all clients";

            return options;
        });

        app.MapPost("/notify-client-error", async (IHubContext<NotificationHub> hubContext, Notification notification) =>
        {
            await hubContext.Clients.All.SendAsync("ReceiveNotification", notification.Message, "Error");
            return Results.NoContent();
        })
        .Produces(StatusCodes.Status204NoContent)
        .WithOpenApi(options =>
        {
            options.Tags = [new OpenApiTag { Name = "Notifications" }];
            options.Summary = "Send a notification to all clients";
            options.Description = "Send a notification to all clients";

            return options;
        });

        app.Run();
    }

    private record Notification(string Message);
}