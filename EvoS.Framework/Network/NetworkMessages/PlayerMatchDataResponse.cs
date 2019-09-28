using System;
using System.Collections.Generic;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(683)]
    public class PlayerMatchDataResponse : WebSocketResponseMessage
    {
        public List<PersistedCharacterMatchData> MatchData;
    }
}
