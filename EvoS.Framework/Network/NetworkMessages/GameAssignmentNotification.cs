using System;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(742)]
    public class GameAssignmentNotification : WebSocketMessage
    {
        public LobbyGameInfo GameInfo;
        public LobbyGameplayOverrides GameplayOverrides;
        public LobbyPlayerInfo PlayerInfo;
        public GameResult GameResult;
        public bool Reconnection;
        public bool Observer;
    }
}
