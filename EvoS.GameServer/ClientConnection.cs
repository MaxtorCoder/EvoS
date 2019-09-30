using EvoS.Framework.Logging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using EvoS.GameServer.Network;
using EvoS.GameServer.Network.Unity;
using Newtonsoft.Json;
using WebSocket = vtortola.WebSockets.WebSocket;
using WebSocketMessageType = vtortola.WebSockets.WebSocketMessageType;

namespace EvoS.GameServer
{
    public class ClientConnection
    {
        private WebSocket Socket;

        public ClientConnection(WebSocket socket)
        {
            Socket = socket;
        }

        public void Disconnect()
        {
            Log.Print(LogType.GameServer, "Client disconnected.");
            if (Socket.IsConnected)
                Socket.Close();

            Socket.Dispose();
        }

        public async Task SendMessage(object message)
        {
            throw new NotImplementedException();
        }

        public async void HandleConnection()
        {
            while (true)
            {
                var message = await Socket.ReadMessageAsync(CancellationToken.None);

                if (message == null)
                {
                    Log.Print(LogType.Warning, "Message is null");
                    Disconnect();
                    return;
                }

                if (message.MessageType != WebSocketMessageType.Binary)
                {
                    throw new NotImplementedException();
                }

                using (var ms = new MemoryStream())
                {
                    await message.CopyToAsync(ms);
                    await ms.FlushAsync();

                    var messageBytes = ms.ToArray();

                    foreach (var msg in UNetSerializer.Instance.ProcessUNetMessage(messageBytes))
                    {
                        Log.Print(LogType.GameServer, $"{msg.GetType().Name} - {JsonConvert.SerializeObject(msg)}");
                    }
                }

                message.Dispose();
            }
        }
    }
}
