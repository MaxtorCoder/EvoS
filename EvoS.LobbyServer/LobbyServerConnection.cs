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
using EvoS.Framework.Misc;
using EvoS.Framework.Constants.Enums;

namespace EvoS.LobbyServer
{
    public class LobbyServerConnection : IComparable
    {
        private WebSocket Socket;
        public string StatusString;

        public BotDifficulty AllyBotDifficulty;
        public BotDifficulty EnemyBotDifficulty;

        public SessionPlayerInfo PlayerInfo;

        public long SessionToken;

        public static Dictionary<Type, MethodBase> LobbyManagerHandlers = new Dictionary<Type, MethodBase>();
        

        public LobbyServerConnection(WebSocket socket)
        {
            Socket = socket;
        }

        public String ToString() {
            return PlayerInfo.GetHandle();
        }

        public void Disconnect()
        {
            Log.Print(LogType.Lobby, $"Client {PlayerInfo.GetHandle()} disconnected.");
            OnDisconnect.Invoke(this, new EventArgs());
            if (Socket.IsConnected)
                Socket.Close();
            Socket.Dispose();
        }

        public async Task SendMessage(object message)
        {
            var responseStream = new MemoryStream();
            EvosSerializer.Instance.Serialize(responseStream, message);
            Console.WriteLine("SendMessage("+ PlayerInfo.GetHandle() + "): " + message.ToString());
            lock (Socket){
                using (var writer = Socket.CreateMessageWriter(WebSocketMessageType.Binary))
                {
                    writer.WriteAsync(responseStream.ToArray());
                    writer.FlushAsync();
                }
            }
            
        }

        public event EventHandler OnDisconnect;

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
                        MethodBase method = GetHandler(requestType);

                        if (method == null) {
                            Log.Print(LogType.Error, $"Unhandled type: {requestType.Name}");
                        } else {
                            Task task = (Task)method.Invoke(null, new object[] { this, requestData });
                            try
                            {
                                await task.ConfigureAwait(false);
                                await task;
                            }
                            catch (vtortola.WebSockets.WebSocketException e) {
                                Log.Print(LogType.Error, PlayerInfo.GetHandle() + "::" + e.Message);
                                Disconnect();
                            }
                            
                        }
                    }
                }
                //Console.WriteLine($"RECV {message.MessageType} {(message.MessageType == WebSocketMessageType.Text ? message.ReadText() : message.ReadBinary())}");
                message.Dispose();
            }
        }

        public LobbyPlayerInfo GetLobbyPlayerInfo()
        {
            return this.PlayerInfo.GetLobbyPlayerInfo();
        }

        private static MethodBase GetHandler(Type type)
        {
            MethodBase method = null;
            lock (LobbyManagerHandlers)
            {
                try
                {
                    method = LobbyManagerHandlers[type];

                }
                catch (KeyNotFoundException e)
                {

                    MethodInfo[] methods = typeof(LobbyManager).GetMethods(BindingFlags.Static | BindingFlags.Public);
                    method = Type.DefaultBinder.SelectMethod(BindingFlags.Default, methods, new Type[] { typeof(LobbyServerConnection), type }, null);
                    if (method != null)
                    {
                        LobbyManagerHandlers.Add(type, method);
                        //Log.Print(LogType.Debug, $"Added method for {type.Name}: {method.ToString()}");
                    }

                }
            }
            

            return method;
        }

        public int CompareTo(object obj)
        {
            if (((LobbyServerConnection)obj).SessionToken - this.SessionToken == 0) return 0;
            else if(((LobbyServerConnection)obj).SessionToken > this.SessionToken) return -1;
            return 1;
        }
    }
}
