using EvoS.Framework;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using vtortola.WebSockets;
using vtortola.WebSockets.Rfc6455;
using System.IO;

namespace EvoS.LobbyServer
{
    class Program
    {
        static ILog Log = new Log();
        private static List<ClientConnection> ConnectedClients = new List<ClientConnection>();

        static void Main(string[] args)
        {
            Task server = Task.Run(StartServer);
            server.Wait();
            Log.Print(LogType.Server, "Server Stopped");
        }

        private static async Task StartServer()
        {
            Console.WriteLine("Starting server");
            WebSocketListener server = new WebSocketListener(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6060));
            server.Standards.RegisterStandard(new WebSocketFactoryRfc6455());

            // Server doesnt start if i await StartAsync...
#pragma warning disable CS4014
            server.StartAsync();
#pragma warning restore CS4014

            Log.Print(LogType.Server, "Started webserver on '0.0.0.0:6060'");

            while (true)
            {
                Log.Print(LogType.Server, "Waiting for clients to connect...");
                WebSocket socket = await server.AcceptWebSocketAsync(CancellationToken.None);
                Log.Print(LogType.Server, "Client connected");
                ClientConnection newClient = new ClientConnection(socket);
                ConnectedClients.Add(newClient);

                new Thread(newClient.HandleConnection).Start();
            }
        }

        private static async void HandleClient(object arg)
        {
            WebSocket client = (WebSocket)arg;
            while (true)
            {
                WebSocketMessageReadStream message = await client.ReadMessageAsync(CancellationToken.None);
                if (!client.IsConnected || message == null)
                {
                    Log.Print(LogType.Server, "a client disconnected");
                    client.Dispose();
                    return;
                }
                Console.WriteLine($"RECV {message.MessageType} {(message.MessageType == WebSocketMessageType.Text ? message.ReadText() : message.ReadBinary())}");
                message.Dispose();
            }

        }
    }
}
