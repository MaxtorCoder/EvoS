using System.Threading.Tasks;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Logging;
using EvoS.Framework.Network.NetworkMessages;
using Newtonsoft.Json;

namespace EvoS.LobbyServer.NetworkMessageHandlers
{
    class PlayerInfoUpdateRequestHandler : IEvosNetworkMessageHandler
    {
        public async Task OnMessage(ClientConnection connection, object requestData)
        {
            PlayerInfoUpdateRequest request = (PlayerInfoUpdateRequest) requestData;

            if (request.PlayerInfoUpdate.ContextualReadyState != null)
            {
                if (request.PlayerInfoUpdate.ContextualReadyState.Value.ReadyState == ReadyState.Ready)
                {
                    var practice = DummyLobbyData.CreatePracticeGameNotification(connection);
                    Log.Print(LogType.Network, $"Responding {JsonConvert.SerializeObject(practice)}");
                    await connection.SendMessage(practice);
                    
                    var practiceGameInfo = DummyLobbyData.CreatePracticeGameInfoNotification(connection);
                    Log.Print(LogType.Network, $"Responding {JsonConvert.SerializeObject(practiceGameInfo)}");
                    await connection.SendMessage(practiceGameInfo);
                    return;
                }
            }

            if (request.PlayerInfoUpdate.CharacterType == null)
            {
                Log.Print(LogType.Warning, "CharacterType is null in PlayerInfoUpdateRequest");
                return;
            }

            var accountDataUpdate = new PlayerAccountDataUpdateNotification
            {
                AccountData = DummyLobbyData.CreateAccountData(connection)
            };
            accountDataUpdate.AccountData.AccountComponent.LastCharacter = request.PlayerInfoUpdate.CharacterType.Value;
            Log.Print(LogType.Network, $"Responding {JsonConvert.SerializeObject(accountDataUpdate)}");
            await connection.SendMessage(accountDataUpdate);

            var response = new PlayerInfoUpdateResponse
            {
                CharacterInfo = DummyLobbyData.CreateLobbyCharacterInfo(request.PlayerInfoUpdate.CharacterType.Value),
                // PlayerInfo = DummyLobbyData.CreateLobbyPlayerInfo(),
                OriginalPlayerInfoUpdate = request.PlayerInfoUpdate,
                ResponseId = request.RequestId
            };

            if (request.PlayerInfoUpdate.CharacterCards != null)
                response.CharacterInfo.CharacterCards = request.PlayerInfoUpdate.CharacterCards.Value;

            if (request.PlayerInfoUpdate.CharacterMods != null)
                response.CharacterInfo.CharacterMods = request.PlayerInfoUpdate.CharacterMods.Value;

            // response.PlayerInfo.PlayerId = request.PlayerInfoUpdate.PlayerId;
            // response.PlayerInfo.CharacterInfo = response.CharacterInfo;
            Log.Print(LogType.Network, $"Responding {JsonConvert.SerializeObject(response)}");
            await connection.SendMessage(response);
        }
    }
}
