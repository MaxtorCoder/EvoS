using EvoS.Framework.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using vtortola.WebSockets;
using vtortola.WebSockets.Rfc6455;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Network;
using EvoS.Framework.Network.WebSocket;
using EvoS.Framework.Network.NetworkMessages;
using EvoS.LobbyServer.NetworkMessageHandlers;
using System.Reflection;

namespace EvoS.LobbyServer
{
    class ClientConnection
    {
        private WebSocket Socket;

        public ClientConnection(WebSocket socket)
        {
            Socket = socket;
        }

        public void Disconnect()
        {
            Log.Print(LogType.Server, "Client disconnected.");
            if (Socket.IsConnected)
                Socket.Close();

            Socket.Dispose();
        }

        public async void HandleConnection()
        {
            Console.WriteLine("Handling Connection");
            WebSocketMessageWriteStream writeStream = Socket.CreateMessageWriter(WebSocketMessageType.Binary);

            while (true)
            {
                Console.WriteLine("while");
                WebSocketMessageReadStream message = await Socket.ReadMessageAsync(CancellationToken.None);

                if (message == null)
                {
                    Console.WriteLine("Message is null");
                    writeStream.Dispose();
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

                        ms.Seek(0, SeekOrigin.Begin);
                        object requestData = EvosSerializer.Instance.Deserialize(ms);
                        Type requestType = requestData.GetType();
                        Log.Print(LogType.Network, $"Received {requestType.Name}");

                        // Create Response
                        Type responseHandlerType = Type.GetType($"EvoS.LobbyServer.NetworkMessageHandlers.{requestType.Name}Handler");
                        object responseHandler = responseHandlerType.GetConstructor(Type.EmptyTypes).Invoke(new object[] { });
                        var responseStream = new MemoryStream();
                        responseHandlerType.GetMethod("OnMessage").Invoke(responseHandler, new[] { requestData, responseStream });

                        // Write Response
                        responseStream.Seek(0, SeekOrigin.Begin);
                        await responseStream.CopyToAsync(writeStream);
                        await writeStream.FlushAsync();
                    }

                }
                //Console.WriteLine($"RECV {message.MessageType} {(message.MessageType == WebSocketMessageType.Text ? message.ReadText() : message.ReadBinary())}");
                message.Dispose();
            }
        }
    }
}
