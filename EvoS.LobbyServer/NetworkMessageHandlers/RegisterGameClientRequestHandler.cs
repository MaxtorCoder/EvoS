using EvoS.Framework.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Logging;
using EvoS.Framework.Network.NetworkMessages;
using EvoS.Framework.Network.Static;
using EvoS.LobbyServer.Utils;
using EvoS.Framework.DataAccess;

namespace EvoS.LobbyServer.NetworkMessageHandlers
{
    /// <summary>
    /// First message sent to the server when a user connects
    /// </summary>
    class RegisterGameClientRequestHandler : IEvosNetworkMessageHandler
    {
        public async Task OnMessage(LobbyServerConnection connection, object requestData)
        {
            RegisterGameClientRequest request = (RegisterGameClientRequest) requestData;
            PlayerData.Player p = PlayerData.GetPlayer(request.SessionInfo.Handle);

            connection.SessionToken = request.SessionInfo.SessionToken;

            //connection.PlayerInfo = SessionManager.Get(connection.SessionToken);
            //connection.PlayerInfo.SetHandle(p.UserName);
            //connection.PlayerInfo.SetAccountId(p.AccountId);
            //connection.PlayerInfo.SetBannerID(p.SelectedBackgroundBannerID);
            //connection.PlayerInfo.SetEmblemID(p.SelectedForegroundBannerID);
            //connection.PlayerInfo.SetRibbonID(p.SelectedRibbonID);
            //connection.PlayerInfo.SetTitleID(p.SelectedTitleID);
            //connection.PlayerInfo.SetCharacterType(p.LastSelectedCharacter);

            // Send RegisterGameClientResponse
            await Send_RegisterGameClientResponse(connection, request);

            // Sent LobbyServerReadyNotification
            await Send_LobbyServerReadyNotification(connection);

            // Send "{username} has connected" to global chat
            await Send_ChatConnectedNotification(connection);
        }


        private async Task Send_RegisterGameClientResponse(LobbyServerConnection connection, RegisterGameClientRequest request)
        {
            Log.Print(LogType.Debug, "Sending Send_RegisterGameClientResponse");
            var response = RegisterGameClient(request);
            await connection.SendMessage(response);
        }

        private async Task Send_LobbyServerReadyNotification(LobbyServerConnection connection)
        {
            LobbyServerReadyNotification lobbyServerReady = LobbyServerReady(connection);
            await connection.SendMessage(lobbyServerReady);
        }

        private async Task Send_ChatConnectedNotification(LobbyServerConnection connection)
        {
            //ChatNotification connectedMessage = new ChatNotification() { Text = $"{connection.PlayerInfo.GetHandle()} has connected", ConsoleMessageType = ConsoleMessageType.SystemMessage };
            //await LobbyServer.sendChatToAll(connectedMessage);
        }


        private LobbyServerReadyNotification LobbyServerReady(LobbyServerConnection connection)
        {
            // search here
            return new LobbyServerReadyNotification
            {
                AccountData = PlayerUtils.GetAccountData(connection),
                AlertMissionData = new LobbyAlertMissionDataNotification(),
                CharacterDataList = DummyLobbyData.CreateCharacterDataList(),
                CommerceURL = "http://127.0.0.1/AtlasCommerce",
                EnvironmentType = EnvironmentType.External,
                FactionCompetitionStatus = new FactionCompetitionNotification(),
                FriendStatus = null,//new FriendStatusNotification {FriendList = FriendData.GetFriendList(connection.AccountId)},
                GroupInfo = new LobbyPlayerGroupInfo
                {
                    SelectedQueueType = GameType.PvP,
                    //MemberDisplayName = connection.PlayerInfo.GetHandle(),
                    //ChararacterInfo = DummyLobbyData.CreateLobbyCharacterInfo(CharacterType.Archer),
                    Members = new List<UpdateGroupMemberData>()
                },
                SeasonChapterQuests =
                    new LobbySeasonQuestDataNotification
                    {
                        SeasonChapterQuests = new Dictionary<int, SeasonChapterQuests>()
                    },
                ServerQueueConfiguration = DummyLobbyData.CreateServerQueueConfiguration(),
                Status = new LobbyStatusNotification
                {
                    AllowRelogin = false,
                    ServerLockState = ServerLockState.Unlocked,
                    ServerMessageOverrides = null,//new ServerMessageOverrides(),
                    ClientAccessLevel = ClientAccessLevel.Full,
                    HasPurchasedGame = true,
                    GameplayOverrides = DummyLobbyData.CreateLobbyGameplayOverrides(),
                    UtcNow = DateTime.UtcNow,
                    PacificNow = DateTime.Now, // TODO: Should be pacific time
                    ErrorReportRate = TimeSpan.FromMinutes(3)
                }
            };
        }

        private static RegisterGameClientResponse RegisterGameClient(RegisterGameClientRequest request)
        {
            RegisterGameClientResponse response = new RegisterGameClientResponse();

            response.SessionInfo = request.SessionInfo;
            response.SessionInfo.ConnectionAddress = "127.0.0.1";
            response.SessionInfo.LanguageCode = "EN";
            response.AuthInfo = request.AuthInfo;
            response.AuthInfo.AccountId = request.SessionInfo.AccountId; // Override AuthInfo.AccountId with SessionInfo.AccountID, The account id from SessionInfo is set in the DirectoryServer and has the accountid value from database for the client username
            response.DevServerConnectionUrl = "127.0.0.1"; // What is this?
            response.AuthInfo.AccountStatus = null;
            response.Status = new LobbyStatusNotification
            {
                AllowRelogin = false,
                ClientAccessLevel = ClientAccessLevel.Full,
                ConnectionQueueInfo = null,
                ErrorReportRate = TimeSpan.FromMinutes(3),
                GameplayOverrides = null,
                HasPurchasedGame = true,
                HighestPurchasedGamePack = 0,
                LocalizedFailure = null,
                PacificNow = DateTime.UtcNow, // TODO: Originally had "0001-01-01T00:00:00"
                ServerLockState = ServerLockState.Unlocked,
                ServerMessageOverrides = new ServerMessageOverrides().FillDummyData(),
                TimeOffset = TimeSpan.Zero,
                UtcNow = DateTime.UtcNow, // TODO: Originally had "0001-01-01T00:00:00"
                RequestId = 0,
                ResponseId = 0,
            };
            response.LocalizedFailure = null;
            response.RequestId = 0;
            response.ResponseId = request.RequestId;
            return response;
        }
    }
}
