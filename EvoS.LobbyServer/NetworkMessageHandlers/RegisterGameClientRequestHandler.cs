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

namespace EvoS.LobbyServer.NetworkMessageHandlers
{
    class RegisterGameClientRequestHandler : IEvosNetworkMessageHandler
    {
        public bool DoLogPacket() { return true; }

        public async Task OnMessage(ClientConnection connection, object requestData)
        {
            RegisterGameClientRequest request = (RegisterGameClientRequest) requestData;
            connection.RegistrationInfo = request;
            
            var response = RegisterGameClient(request);
            await connection.SendMessage(response);
            var lobbyServerReady = LobbyServerReady(request);
            await connection.SendMessage(lobbyServerReady);
        }

        private LobbyServerReadyNotification LobbyServerReady(RegisterGameClientRequest request)
        {
            return new LobbyServerReadyNotification
            {
                AccountData = new PersistedAccountData
                {
                    QuestComponent = new QuestComponent {ActiveSeason = 9},
                    AccountId = request.AuthInfo.AccountId,
                    UserName = request.AuthInfo.UserName,
                    Handle = request.AuthInfo.Handle,
                    SchemaVersion = new SchemaVersion<AccountSchemaChange>(0x1FFFF),
                    CreateDate = DateTime.Now.AddHours(-1),
                    UpdateDate = DateTime.Now,
                },
                AlertMissionData = new LobbyAlertMissionDataNotification(),
                CharacterDataList = DummyLobbyData.CreateCharacterDataList(),
                CommerceURL = "http://127.0.0.1/AtlasCommerce",
                EnvironmentType = EnvironmentType.External,
                FactionCompetitionStatus = new FactionCompetitionNotification(),
                FriendStatus = new FriendStatusNotification {FriendList = new FriendList()},
                GroupInfo = new LobbyPlayerGroupInfo
                {
                    SelectedQueueType = GameType.Practice,
                    MemberDisplayName = request.SessionInfo.Handle,
//                    ChararacterInfo = DummyLobbyData.CreateLobbyCharacterInfo(CharacterType.Archer),
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
                    ServerMessageOverrides = new ServerMessageOverrides(),
                    ClientAccessLevel = ClientAccessLevel.Full,
                    HasPurchasedGame = true,
                    GameplayOverrides = new LobbyGameplayOverrides
                    {
                        CharacterConfigs = DummyLobbyData.CreateCharacterConfigs()
                    },
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
            response.AuthInfo = request.AuthInfo;
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
                ServerMessageOverrides = null,
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
