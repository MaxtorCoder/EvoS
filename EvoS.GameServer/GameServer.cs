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
using EvoS.Framework.Network;

namespace EvoS.GameServer
{
    class GameServer
    {
        private static List<ClientConnection> ConnectedClients = new List<ClientConnection>();

        static void Main(string[] args)
        {
            Task server = Task.Run(StartServer);
            server.Wait();
            Log.Print(LogType.Server, "Server Stopped");
        }

        private static async Task StartServer()
        {
            Log.Print(LogType.Server, "Starting server");
            WebSocketListener server = new WebSocketListener(new IPEndPoint(IPAddress.Parse("0.0.0.0"), 6061));
            server.Standards.RegisterStandard(new WebSocketFactoryRfc6455());

            // Server doesnt start if i await StartAsync...
#pragma warning disable CS4014
            server.StartAsync();
#pragma warning restore CS4014

            Log.Print(LogType.Server, "Started webserver on '0.0.0.0:6061'");

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
    }
}
