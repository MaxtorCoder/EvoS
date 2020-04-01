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
        public async Task OnMessage(ClientConnection connection, object requestData)
        {
            PlayerInfoUpdateRequest request = (PlayerInfoUpdateRequest) requestData;
            if (request.GameType != null)
                connection.SelectedGameType = request.GameType.Value;


            // Change ReadyState
            if (request.PlayerInfoUpdate.ContextualReadyState != null)
            {
                if (request.PlayerInfoUpdate.ContextualReadyState.Value.ReadyState == ReadyState.Ready)
                {
                    //LobbyQueueManager.HandleReady(connection);
                    Log.Print(LogType.Debug, "OLD HANDLE CHANGE READYSTATE");
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
                connection.SelectedCharacter = request.PlayerInfoUpdate.CharacterType.Value;
                PlayerData.SaveSelectedCharacter(connection.AccountId, (int)connection.SelectedCharacter);

                var accountDataUpdate = new PlayerAccountDataUpdateNotification
                {
                    AccountData = PlayerUtils.GetAccountData(connection)
                };

                await connection.SendMessage(accountDataUpdate);
                var response = new PlayerInfoUpdateResponse
                {
                    CharacterInfo = DummyLobbyData.CreateLobbyCharacterInfo(connection.SelectedCharacter),//TODO
                    OriginalPlayerInfoUpdate = request.PlayerInfoUpdate,
                    ResponseId = request.RequestId
                };
                await connection.SendMessage(response);
                return;
            }

            //Change Skin
            else if (request.PlayerInfoUpdate.CharacterSkin != null)
            {
                connection.Loadout.Skin = request.PlayerInfoUpdate.CharacterSkin.Value;
            }
            //Chnage Catalyst
            else if (request.PlayerInfoUpdate.CharacterCards != null)
            {
                connection.Loadout.Cards = request.PlayerInfoUpdate.CharacterCards.Value;
            }
            // Chnage Mods
            else if (request.PlayerInfoUpdate.CharacterMods != null)
            {
                connection.Loadout.Mods = request.PlayerInfoUpdate.CharacterMods.Value;
            }
            // Change Vfx
            else if (request.PlayerInfoUpdate.CharacterAbilityVfxSwaps != null)
            {
                connection.Loadout.AbilityVfxSwaps = request.PlayerInfoUpdate.CharacterAbilityVfxSwaps.Value;
            }
            // Chnage Loadout
            else if (request.PlayerInfoUpdate.CharacterLoadoutChanges != null)
            {
                Log.Print(LogType.Warning, "Chnages in loadout not implemented (PlayerInfoUpdateRequestHandler.cs)");
            }

        }
    }
}
