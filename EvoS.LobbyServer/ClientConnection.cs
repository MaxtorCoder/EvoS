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
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EvoS.LobbyServer
{
    public class ClientConnection
    {
        private WebSocket Socket;
        public RegisterGameClientRequest RegistrationInfo;
        public AuthInfo AuthInfo => RegistrationInfo.AuthInfo;
        public LobbySessionInfo SessionInfo => RegistrationInfo.SessionInfo;

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

        public async Task SendMessage(object message)
        {
            var responseStream = new MemoryStream();
            EvosSerializer.Instance.Serialize(responseStream, message);
            
            using (var writer = Socket.CreateMessageWriter(WebSocketMessageType.Binary))
            {
                await writer.WriteAsync(responseStream.ToArray());
                await writer.FlushAsync();
            }    
        }

        public async void HandleConnection()
        {
            Log.Print(LogType.Debug, "Handling Connection");
            BindingFlags bindingFlags = BindingFlags.FlattenHierarchy | BindingFlags.Public | BindingFlags.Static;

            while (true)
            {
                WebSocketMessageReadStream message = await Socket.ReadMessageAsync(CancellationToken.None);

                if (message == null)
                {
                    Log.Print(LogType.Debug, "Message is null");
                    Disconnect();
                    return;
                }

                if (message.MessageType == WebSocketMessageType.Text)
                {
                    var msgContent = string.Empty;
                    using (var sr = new StreamReader(message, Encoding.UTF8))
                        msgContent = await sr.ReadToEndAsync();
                    Log.Print(LogType.Debug, msgContent);
                }
                else if (message.MessageType == WebSocketMessageType.Binary)
                {
                    using (var ms = new MemoryStream())
                    {
                        await message.CopyToAsync(ms);
                        await ms.FlushAsync();

                        ms.Seek(0, SeekOrigin.Begin);
                        object requestData;

                        try
                        {
                            requestData = EvosSerializer.Instance.Deserialize(ms);
                        }
                        catch (Exception e)
                        {
                            Log.Print(LogType.Error, Convert.ToBase64String(ms.ToArray()));
                            Log.Print(LogType.Error, e.ToString());
                            continue;
                        }

                        Type requestType = requestData.GetType();
#if DEBUG
                        //Log data or hide it?
                        String packetDataToLog = (bool)requestType.GetField("LogData", bindingFlags).GetValue(null) ? JsonConvert.SerializeObject(requestData) :"{...}";

                        Log.Print(LogType.Network, $"Received {requestType.Name} : {packetDataToLog}");
#endif
                        // Handle Response
                        Type responseHandlerType = Type.GetType($"EvoS.LobbyServer.NetworkMessageHandlers.{requestType.Name}Handler");
                        if (responseHandlerType == null)
                        {
#if DEBUG
                            Log.Print(LogType.Warning, $"No Handler for {requestType.Name}");
#endif
                            continue;
                        }

                        object responseHandler = responseHandlerType.GetConstructor(Type.EmptyTypes).Invoke(new object[] { });
                        var task = (Task) responseHandlerType.GetMethod("OnMessage").Invoke(responseHandler, new[] { this, requestData });
                        await task.ConfigureAwait(false);
                        await task;
                    }
                }
                //Console.WriteLine($"RECV {message.MessageType} {(message.MessageType == WebSocketMessageType.Text ? message.ReadText() : message.ReadBinary())}");
                message.Dispose();
            }
        }
    }
}
