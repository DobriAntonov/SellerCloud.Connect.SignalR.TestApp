using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using SellerCloud.Connect.SignalR.Concrete;
using SellerCloud.Connect.SignalR.Contracts;
using SellerCloud.Connect.SignalR.Extensions;
using Microsoft.AspNetCore.SignalR.Client;
using SellerCloud.Connect.SignalR.Policies;
using SellerCloud.Connect.Rest.Results;

namespace SellerCloud.Connect.SignalR.TestApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Guid correlationId = Guid.NewGuid();
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6InN0YW5pbWlyQHNlbGxlcmNsb3VkLmNvbSIsInN1YiI6IloxVVV4TG1GMVJ1RE5mOENkN1dmWUE9PSIsImh0dHA6Ly9zY2hlbWFzLnNlbGxlcmNsb3VkLmNvbS93cy8yMDE5LzAxL2lkZW50aXR5L2NsYWltcy9jbGllbnRfaWQiOiJSd0swSWNWNVJkQVdQWkpwVEo2c1dRPT0iLCJodHRwOi8vc2NoZW1hcy5zZWxsZXJjbG91ZC5jb20vd3MvMjAxOS8wMS9pZGVudGl0eS9jbGFpbXMvcmVhbG1faGFzaCI6ImluTzhIeTF4MjIwWHJIVmwzSWlIekdrQloxUkRRSkdvUFpKSmRoYWZTVWs9Iiwicm9sZSI6WyJBZG1pbmlzdHJhdG9yIiwiQW1hem9uIEFkbWluIiwiQXBwbHkgRGlzY291bnQgb24gT3JkZXIiLCJBcHByb3ZlIFBPIiwiQ2FuY2VsIEV4cG9ydGVkIE9yZGVyIiwiQ3JlYXRlIFJNQSIsIkN1c3RvbWVyIiwiRHJvcHNoaXAgTWFuYWdlciIsImVCYXkgTWVyY2hhbnQiLCJFZGl0IFBPIiwiRWRpdCBQdXJjaGFzZXIiLCJFbXBsb3llZSIsIkludmVudG9yeSBNYW5hZ2VyIiwiTG9naW4gTmV2ZXIgRXhwaXJlcyIsIk9yZGVyIEFkbWluIiwiT3JkZXIgQ2FuY2VsIiwiT3JkZXIgQ29udHJvbGxlciIsIk9yZGVyIENvbnRyb2xsZXIgTG9jYWwiLCJPcmRlciBDcmVhdG9yIiwiT3JkZXIgVG90YWwgZGlzcGxheSIsIk9yZGVycyBNYXNzIEVtYWlsZXIiLCJQZW9wbGUgQWRtaW4iLCJQTyBQcmljZXMiLCJQcmljaW5nIEFkbWluIiwiUHJpY2luZyBNYW5hZ2VyIiwiUHJvZHVjdCBBZG1pbiIsIlByb2R1Y3QgS2l0IENyZWF0b3IiLCJQcm9kdWN0IFNoYWRvdyBDcmVhdG9yIiwiUmVmdW5kSGFuZGxlciIsIlJlcG9ydCBBZG1pbiIsIlJlcG9ydCBVc2VycyIsIlJlc3RyaWN0IEZCQVNoaXBtZW50cyBwZXIgQnV5ZXIiLCJSZXN0cmljdCBJbnZlbnRvcnkgQWNjZXNzIiwiUmVzdHJpY3QgTGlzdGluZyBQcm9kdWN0IiwiUmVzdHJpY3QgT3JkZXIgcGVyIEJ1eWVyIiwiUmVzdHJpY3QgUE8gcGVyIEJ1eWVyIiwiUmVzdHJpY3QgUHJpY2UvUXR5IEVkaXQiLCJSZXN0cmljdCBQcm9kdWN0IENyZWF0ZSIsIlJlc3RyaWN0IFByb2R1Y3QgRWRpdCIsIlJNQSBSZWNlaXZlIiwiU2FsZXMgUmVwIENvc3QgRWRpdCIsIlNhbGVzIFJlcCBDb3N0IFZpZXciLCJTY2hlZHVsZWQgVGFza3MiLCJTZXR0aW5ncyBBZG1pbiIsIlNob3cgQWRqdXN0ZWQgU2hpcHBpbmcgUHJpY2UiLCJTcGxpdCBTS1UgRGV0YWlsIiwiU3BsaXQgU0tVIExpc3QiLCJWZW5kb3IgTWFuYWdlciIsIlZpZXcgUE8iLCJXZWIgU2VydmljZXMgQWNjZXNzIiwiV2hvbGVTYWxlIEN1c3RvbWVyIEhhbmRsZXIiXSwianRpIjoiYjgyMDYxMTItYmY0NS00ZTU4LTllOGUtNTcxNDA2ZmM2ZDlhIiwibmJmIjoxNTc4ODQzNzYyLCJleHAiOjE1NzkxMDI5NjEsImlhdCI6MTU3ODg0Mzc2MiwiaXNzIjoiU2VsbGVyQ2xvdWQiLCJhdWQiOiJTZWxsZXJDbG91ZCJ9.PQXWPEhrEcdVq09vgyE1cxkkqfzp3gng7Rx5G3lRL8k";

            await TestEventsHub("https://localhost:44318", token, correlationId);

            Console.WriteLine("Hello World!");
        }

        static async Task TestEventsHub(string apiServer, string token, Guid correlationId)
        {
            Task<string> GetAccessTokenAsync() => Task.FromResult(token); // TODO: Actually acquire a token

            Uri hubUri = new Uri($"{apiServer}/hubs/events").WithScheme("wss");

            HubConnection connection = new HubConnectionBuilder()
                .WithUrl(hubUri, options => options.AccessTokenProvider = GetAccessTokenAsync)
                .WithAutomaticReconnect(new InfiniteRandomRetryPolicy())
                .Build();

            IEventsHubClient hub = new EventsHubClient(connection);

            AttachConnectionEventHandlers(connection);

            AttachHubEventHandlers(hub);

            await ConnectAsync(connection, hubUri, new InfiniteRandomRetryPolicy());

            // First action on reconnect
            connection.Reconnected += _ => SubscribeAndReactToEvents(hub, correlationId);

            // First action on initial connect
            await SubscribeAndReactToEvents(hub, correlationId);

            await StopAndCleanupAsync(connection);
        }

        static void AttachConnectionEventHandlers(HubConnection connection)
        {
            // ?? connection.Connecting += xxxx;
            // ?? connection.Connected += xxxx;

            connection.Closed += error => StatusAsync(error == null ? "Disconnected" : $"Disconnected with error: {error}");

            connection.Reconnecting += error => StatusAsync($"Reconnecting... ({error.Message})");

            connection.Reconnected += connectionId => StatusAsync($"Reconnected! ({connectionId})");
        }
        static Task StatusAsync(string status)
        {
            string timestamp = $"{DateTime.Now:MM/dd/yyyy hh:mm:ss tt}";

            Console.WriteLine($"{timestamp} {status}");

            return Task.CompletedTask;
        }

        static void AttachHubEventHandlers(IEventsHubClient hub)
        {
            hub.SendTokenResult += (correlationId, token) => StatusAsync($"Token recieved '{token}' for correlation id {correlationId}.");
        }

        static async Task ConnectAsync(HubConnection connection, Uri hubUri, IRetryPolicy retryPolicy)
        {
            await StatusAsync($"Connecting to {hubUri}...");

            await retryPolicy.UsePolicy(() => connection.StartAsync());

            await StatusAsync($"Connected! ({connection.ConnectionId})");
        }

        static async Task SubscribeAndReactToEvents(IEventsHubClient hub, Guid correlationId)
        {
            await StatusAsync($"Subscribing to events for {correlationId}...");

            bool subscribeResult = await hub.SubscribeAsync(correlationId);

            //subscribeResult.ThrowIfUnsuccessful();

            await StatusAsync($"Subscribed!");

            Console.WriteLine();
            Console.WriteLine("(Press Enter to exit)");
            Console.WriteLine();

            Console.ReadLine();

            Result unsubscribeResult = await hub.UnsubscribeAsync(correlationId);

            unsubscribeResult.ThrowIfUnsuccessful();
        }

        static async Task StopAndCleanupAsync(HubConnection connection)
        {
            await StatusAsync($"Stopping... ({connection.ConnectionId})");

            await connection.StopAsync();

            await StatusAsync("Stopped");

            await connection.DisposeAsync();
        }

    }
}