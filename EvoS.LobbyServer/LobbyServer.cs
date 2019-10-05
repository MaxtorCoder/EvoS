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
using EvoS.Framework.Network;
using EvoS.Framework.Network.NetworkMessages;

namespace EvoS.LobbyServer
{
    public class Program
    {
        private static List<ClientConnection> ConnectedClients = new List<ClientConnection>();

        public static void Main(string[] args = null)
        {
            Task server = Task.Run(StartServer);
            server.Wait();
            Log.Print(LogType.Lobby, "Server Stopped");
        }

        private static async Task StartServer()
        {
            Log.Print(LogType.Lobby, "Starting LobbyServer");
            WebSocketListener server = new WebSocketListener(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6060));
            server.Standards.RegisterStandard(new WebSocketFactoryRfc6455());

            // Server doesnt start if i await StartAsync...
#pragma warning disable CS4014
            server.StartAsync();
#pragma warning restore CS4014

            Log.Print(LogType.Server, "Started LobbyServer on '0.0.0.0:6060'");

            while (true)
            {
                Log.Print(LogType.Lobby, "Waiting for clients to connect...");
                WebSocket socket = await server.AcceptWebSocketAsync(CancellationToken.None);
                Log.Print(LogType.Lobby, "Client connected");
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
                    Log.Print(LogType.Lobby, "A client disconnected");
                    client.Dispose();
                    return;
                }
                Log.Print(LogType.Debug, $"RECV {message.MessageType} {(message.MessageType == WebSocketMessageType.Text ? message.ReadText() : message.ReadBinary())}");
                message.Dispose();
            }

        }

        public static async Task sendChatAsync(ChatNotification chat, ClientConnection sender)
        {
            chat.DisplayDevTag = true;
            chat.SenderHandle = sender.RegistrationInfo.AuthInfo.Handle;

            foreach (ClientConnection con in ConnectedClients) {
                await con.SendMessage(chat);
            }
        }
    }
}
