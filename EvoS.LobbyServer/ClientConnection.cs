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
    public class ClientConnection : IComparable
    {
        private WebSocket Socket;
        public long AccountId;
        public string UserName;
        public string StatusString;
        public CharacterType SelectedCharacter;

        public GameType SelectedGameType;
        public ushort SelectedSubTypeMask;
        public BotDifficulty AllyBotDifficulty;
        public BotDifficulty EnemyBotDifficulty;

        public PlayerLoadout Loadout;
        public int SelectedTitleID;
        public int SelectedForegroundBannerID;
        public int SelectedBackgroundBannerID;
        public int SelectedRibbonID = -1;
        public long SessionToken;

        public static Dictionary<Type, MethodBase> LobbyManagerHandlers = new Dictionary<Type, MethodBase>();
        

        public ClientConnection(WebSocket socket)
        {
            Socket = socket;
            SelectedCharacter = CharacterType.Scoundrel;
            Loadout = new PlayerLoadout();
            SelectedSubTypeMask = 0;
            AllyBotDifficulty = BotDifficulty.Easy;
            EnemyBotDifficulty = BotDifficulty.Easy;
        }

        public String ToString() {
            return this.UserName;
        }

        public void Disconnect()
        {
            Log.Print(LogType.Lobby, $"Client {UserName} disconnected.");
            OnDisconnect.Invoke(this, new EventArgs());
            if (Socket.IsConnected)
                Socket.Close();
            Socket.Dispose();
        }

        public async Task SendMessage(object message)
        {
            var responseStream = new MemoryStream();
            EvosSerializer.Instance.Serialize(responseStream, message);
            Console.WriteLine("SendMessage: " + message.ToString());
            using (var writer = Socket.CreateMessageWriter(WebSocketMessageType.Binary))
            {
                await writer.WriteAsync(responseStream.ToArray());
                await writer.FlushAsync();
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
                            await task.ConfigureAwait(false);
                            await task;
                        }
                    }
                }
                //Console.WriteLine($"RECV {message.MessageType} {(message.MessageType == WebSocketMessageType.Text ? message.ReadText() : message.ReadBinary())}");
                message.Dispose();
            }
        }

        public LobbyPlayerInfo CreateLobbyPlayerInfo()
        {
            LobbyPlayerInfo info = new LobbyPlayerInfo();

            info.AccountId = this.AccountId;
            info.Handle = this.UserName;
            info.TitleID = this.SelectedTitleID;
            info.TitleLevel = 0;

            info.BannerID = this.SelectedBackgroundBannerID;
            info.EmblemID = this.SelectedForegroundBannerID;
            info.RibbonID = this.SelectedRibbonID;
            info.IsGameOwner = true;
            info.IsLoadTestBot = false;
            info.IsNPCBot = false;
            info.BotsMasqueradeAsHumans = false;
            info.CharacterInfo = GetLobbyCharacterInfo();
            //public List<LobbyCharacterInfo> RemoteCharacterInfos = new List<LobbyCharacterInfo>();
            info.EffectiveClientAccessLevel = ClientAccessLevel.Full;

            return info;
        }

        public LobbyCharacterInfo GetLobbyCharacterInfo()
        {
            return new LobbyCharacterInfo()
            {
                CharacterType = this.SelectedCharacter,
                CharacterSkin = this.Loadout.Skin,
                CharacterCards = this.Loadout.Cards,
                CharacterMods = this.Loadout.Mods,
                CharacterAbilityVfxSwaps = this.Loadout.AbilityVfxSwaps,
                CharacterTaunts = this.Loadout.Taunts,
                CharacterLoadouts = new List<CharacterLoadout>() { GetCharacterLoadout() },
                CharacterMatches = 0,
                CharacterLevel = 1
            };
        }

        private static MethodBase GetHandler(Type type)
        {
            MethodBase method = null;

            try
            {
                method = LobbyManagerHandlers[type];
            }
            catch(KeyNotFoundException e)
            {
                
                MethodInfo[] methods = typeof(LobbyManager).GetMethods(BindingFlags.Static | BindingFlags.Public);
                method = Type.DefaultBinder.SelectMethod(BindingFlags.Default, methods, new Type[] { typeof(ClientConnection), type }, null);
                if(method != null) {
                    LobbyManagerHandlers.Add(type, method);
                    //Log.Print(LogType.Debug, $"Added method for {type.Name}: {method.ToString()}");
                }
                
            }

            

            return method;
        }

        public CharacterLoadout GetCharacterLoadout()
        {
            CharacterLoadout ret = new CharacterLoadout(this.Loadout.Mods, this.Loadout.AbilityVfxSwaps, this.Loadout.Name);
            return ret;
        }

        public int CompareTo(object obj)
        {
            if (((ClientConnection)obj).SessionToken - this.SessionToken == 0) return 0;
            else if(((ClientConnection)obj).SessionToken > this.SessionToken) return -1;
            return 1;
        }
    }
}
