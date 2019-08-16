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
        private Dictionary<int, Type> typeDict;
        ILog Log = new Log();

        public ClientConnection(WebSocket socket) {
            Socket = socket;
            
            InitTypeDict();
        }

        private void InitTypeDict() {
            typeDict = new Dictionary<int, Type>();
            typeDict.Add(RegisterGameClientRequest.MessageTypeID, typeof(RegisterGameClientRequest));
        }

        public void Disconnect() {
            Log.Print(LogType.Server, "Client disconnected.");
            if (Socket.IsConnected) {
                Socket.Close();
            }
            Socket.Dispose();
        }

        public async void HandleConnection()
        {
            WebSocketMessageWriteStream writeStream = Socket.CreateMessageWriter(WebSocketMessageType.Binary);

            while (true)
            {
                WebSocketMessageReadStream message = await Socket.ReadMessageAsync(CancellationToken.None);
                Type networkMessage = null;
                
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

                        EvosMessageStream messageStream = new EvosMessageStream(ms);

                        // Get the Type of NetworkMessage received
                        int typeId = messageStream.ReadVarInt();

                        networkMessage = null;
                        typeDict.TryGetValue(typeId, out networkMessage);

                        if (networkMessage != null)
                        {
                            Log.Print(LogType.Network, $"Received {networkMessage.Name}. ID {typeId}");

                            Type handler = Type.GetType($"EvoS.LobbyServer.NetworkMessageHandlers.{networkMessage.Name}Handler");

                            // Create an instance of the handler by calling its constructor
                            object wsm = handler.GetConstructor(Type.EmptyTypes).Invoke(new object[] { });

                            // Log Packet data?
                            if ((bool)(handler.GetMethod("DoLogPacket").Invoke(wsm, new object[] { }))) {
                                byte[] msg = ms.ToArray();
                                Log.Print(LogType.Packet, Encoding.Default.GetString(msg));
                            }

                            // Execute handler.OnMessage()
                            handler.GetMethod("OnMessage").Invoke(wsm, new object[] { messageStream });

                            await messageStream.GetOutputStream().CopyToAsync(writeStream);
                        }
                        else
                        {
                            //Received an unknown/not implemented yet NetworkMessage
                            Log.Print(LogType.Network, "------------- UNKNOWN NETWORK MESSAGE START "+typeId+" ---------------");
                            byte[] msg = ms.ToArray();
                            Log.Print(LogType.Network, Encoding.Default.GetString(msg));
                            Log.Print(LogType.Network, "------------- UNKNOWN NETWORK MESSAGE END ---------------");
                        }
                        
                        
                    }

                }
                //Console.WriteLine($"RECV {message.MessageType} {(message.MessageType == WebSocketMessageType.Text ? message.ReadText() : message.ReadBinary())}");
                message.Dispose();
            }
        }
    }
}
