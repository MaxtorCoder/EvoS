using System;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(753)]
    public class JoinMatchmakingQueueRequest : WebSocketMessage
    {
        public GameType GameType;
        public BotDifficulty AllyBotDifficulty;
        public BotDifficulty EnemyBotDifficulty;
    }
}
