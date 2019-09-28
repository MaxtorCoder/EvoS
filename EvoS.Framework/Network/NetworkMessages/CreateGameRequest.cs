using System;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(759)]
    public class CreateGameRequest : WebSocketMessage
    {
        public LobbyGameConfig GameConfig;
        public ReadyState ReadyState;
        public string ProcessCode;
        public BotDifficulty SelectedBotSkillTeamA;
        public BotDifficulty SelectedBotSkillTeamB;
    }
}
