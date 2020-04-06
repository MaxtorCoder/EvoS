using System;
using System.Threading.Tasks;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.DataAccess;
using EvoS.Framework.Logging;
using EvoS.Framework.Network.NetworkMessages;
using EvoS.LobbyServer.Utils;
using Newtonsoft.Json;

namespace EvoS.LobbyServer.NetworkMessageHandlers
{
    class PlayerInfoUpdateRequestHandler : IEvosNetworkMessageHandler
    {
        public async Task OnMessage(LobbyServerConnection connection, object requestData)
        {
            PlayerInfoUpdateRequest request = (PlayerInfoUpdateRequest) requestData;
            if (request.GameType != null)
                connection.PlayerInfo.SetGameType(request.GameType.Value);


            // Change ReadyState
            if (request.PlayerInfoUpdate.ContextualReadyState != null)
            {
                if (request.PlayerInfoUpdate.ContextualReadyState.Value.ReadyState == ReadyState.Ready)
                {
                    //LobbyQueueManager.HandleReady(connection);
                    Log.Print(LogType.Debug, "HANDLE CHANGE READYSTATE");
                    LobbyQueueManager.AddPlayerToQueue(connection);
                }
                else
                {
                    Log.Print(LogType.Warning, "Contextual Ready state unhandled (PlayerInfoUpdateRequestHandler)");
                }
            }

            // Change Selected Freelancer
            else if (request.PlayerInfoUpdate.CharacterType != null)
            {
                connection.PlayerInfo.SetCharacterType(request.PlayerInfoUpdate.CharacterType.Value);
                PlayerData.SaveSelectedCharacter(connection.PlayerInfo.GetAccountId(), (int)connection.PlayerInfo.GetCharacterType());

                var accountDataUpdate = new PlayerAccountDataUpdateNotification
                {
                    AccountData = PlayerUtils.GetAccountData(connection)
                };

                await connection.SendMessage(accountDataUpdate);
                var response = new PlayerInfoUpdateResponse
                {
                    CharacterInfo = connection.PlayerInfo.GetLobbyPlayerInfo().CharacterInfo,
                    OriginalPlayerInfoUpdate = request.PlayerInfoUpdate,
                    ResponseId = request.RequestId
                };
                await connection.SendMessage(response);
                return;
            }

            //Change Skin
            else if (request.PlayerInfoUpdate.CharacterSkin != null)
            {
                connection.PlayerInfo.SetSkin(request.PlayerInfoUpdate.CharacterSkin.Value);
            }
            //Chnage Catalyst
            else if (request.PlayerInfoUpdate.CharacterCards != null)
            {
                connection.PlayerInfo.SetCards(request.PlayerInfoUpdate.CharacterCards.Value);
            }
            // Chnage Mods
            else if (request.PlayerInfoUpdate.CharacterMods != null)
            {
                connection.PlayerInfo.SetMods(request.PlayerInfoUpdate.CharacterMods.Value);
            }
            // Change Vfx
            else if (request.PlayerInfoUpdate.CharacterAbilityVfxSwaps != null)
            {
                connection.PlayerInfo.SetAbilityVfxSwaps(request.PlayerInfoUpdate.CharacterAbilityVfxSwaps.Value);
            }
            // Chnage Loadout
            else if (request.PlayerInfoUpdate.CharacterLoadoutChanges != null)
            {
                Log.Print(LogType.Warning, "Changes in loadout not implemented yet(PlayerInfoUpdateRequestHandler.cs)");
            }

        }
    }
}
