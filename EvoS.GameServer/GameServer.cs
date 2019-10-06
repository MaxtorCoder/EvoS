﻿using EvoS.Framework.Network.Static;
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
    public class GameServer
    {
        private static List<ClientConnection> ConnectedClients = new List<ClientConnection>();

        public static void Main()
        {
            Task server = Task.Run(StartServer);
            server.Wait();
            Log.Print(LogType.Game, "Server Stopped");
        }

        private static async Task StartServer()
        {
            Log.Print(LogType.Game, "Starting GameServer");
            WebSocketListener server = new WebSocketListener(new IPEndPoint(IPAddress.Parse("0.0.0.0"), 6061));
            server.Standards.RegisterStandard(new WebSocketFactoryRfc6455());

            // Server doesnt start if i await StartAsync...
#pragma warning disable CS4014
            server.StartAsync();
#pragma warning restore CS4014

            Log.Print(LogType.Game, "Started GameServer on '0.0.0.0:6061'");

            while (true)
            {
                Log.Print(LogType.Game, "Waiting for clients to connect...");
                WebSocket socket = await server.AcceptWebSocketAsync(CancellationToken.None);
                Log.Print(LogType.Game, "Client connected");
                ClientConnection newClient = new ClientConnection(socket);
                ConnectedClients.Add(newClient);

                new Thread(newClient.HandleConnection).Start();
            }
        }
    }
}