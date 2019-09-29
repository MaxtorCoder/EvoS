using System;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(755)]
    public class LeaveGameRequest : WebSocketMessage
    {
        public bool IsPermanent;
        public GameResult GameResult;
    }
}
