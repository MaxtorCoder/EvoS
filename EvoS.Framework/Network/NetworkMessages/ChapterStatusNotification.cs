using System;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(439)]
    public class ChapterStatusNotification : WebSocketResponseMessage
    {
        public int SeasonIndex;
        public int ChapterIndex;
        public bool IsCompleted;
        public bool IsUnlocked;
    }
}
