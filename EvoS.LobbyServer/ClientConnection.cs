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
            typeDict.Add(RegisterGameClientResponse.MessageTypeID, typeof(RegisterGameClientResponse));
            typeDict.Add(AssignGameClientRequest.MessageTypeID, typeof(AssignGameClientRequest));
            typeDict.Add(AssignGameClientResponse.MessageTypeID, typeof(AssignGameClientResponse));
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
            while (true)
            {
                WebSocketMessageReadStream message = await Socket.ReadMessageAsync(CancellationToken.None);
                Type networkMessage = null;
                
                if (message == null)
                {
                    Console.WriteLine("Message is null");
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

                        EvosMessageStream ems = new EvosMessageStream(ms);

                        // Get the Type of NetworkMessage received
                        int typeId = ems.ReadVarInt();

                        networkMessage = null;
                        typeDict.TryGetValue(typeId, out networkMessage);

                        if (networkMessage != null)
                        {
                            Console.WriteLine("Received " + networkMessage.Name + "(" + typeId + ")");

                            // Call the HandleMessage method of the received message
                            object wsm = networkMessage.GetConstructor(Type.EmptyTypes).Invoke(new object[] { });
                            MethodInfo handleMessageMethod = networkMessage.GetMethod("HandleMessage");
                            handleMessageMethod.Invoke(wsm, new object[] { ems });

                            //This is the actual message, uncomment to print the received bytes
                            byte[] msg = ms.ToArray();
                            Console.WriteLine(Encoding.Default.GetString(msg));
                            //*/
                        }
                        else
                        {
                            //Received an unknown/not implemented yet NetworkMessage
                            Console.WriteLine("------------- UNKNOWN NETWORK MESSAGE START "+typeId+" ---------------");
                            byte[] msg = ms.ToArray();
                            Console.WriteLine(Encoding.Default.GetString(msg));
                            Console.WriteLine("------------- UNKNOWN NETWORK MESSAGE END ---------------");
                        }
                        
                        
                    }

                }
                //Console.WriteLine($"RECV {message.MessageType} {(message.MessageType == WebSocketMessageType.Text ? message.ReadText() : message.ReadBinary())}");
                message.Dispose();
            }
        }
    }
}
