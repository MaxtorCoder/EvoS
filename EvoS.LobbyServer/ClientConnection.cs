using EvoS.Framework.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using vtortola.WebSockets;
using vtortola.WebSockets.Rfc6455;

namespace EvoS.LobbyServer
{
    class ClientConnection
    {
        private WebSocket Socket;
        ILog Log = new Log();

        public ClientConnection(WebSocket socket) {
            Socket = socket;
        }

        public void Disconnect() {
            Log.Print(LogType.Server, "Client disconnected.");
            if (Socket.IsConnected) {
                Socket.CloseAsync();
            }
            Socket.Dispose();
        }

        public async void HandleConnection()
        {
            while (true)
            {

                WebSocketMessageReadStream message = await Socket.ReadMessageAsync(CancellationToken.None);
                if (message == null)
                {
                    Disconnect();
                    return;
                }
                if (message.MessageType == WebSocketMessageType.Text)
                {
                    var msgContent = string.Empty;
                    using (var sr = new StreamReader(message, Encoding.UTF8))
                        msgContent = await sr.ReadToEndAsync();
                        Console.WriteLine(msgContent);
                }
                else if (message.MessageType == WebSocketMessageType.Binary)
                {
                    using (var ms = new MemoryStream())
                    {
                        await message.CopyToAsync(ms);
                        await ms.FlushAsync();

                        //This is the actual message
                        byte[] msg = ms.ToArray();

                        Console.WriteLine(Encoding.Default.GetString(msg));
                        //Console.WriteLine(msg.Length);
                    }
                }
                //Console.WriteLine($"RECV {message.MessageType} {(message.MessageType == WebSocketMessageType.Text ? message.ReadText() : message.ReadBinary())}");
                message.Dispose();
            }
        }
    }
}
