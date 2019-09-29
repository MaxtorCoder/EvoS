using System;
using System.Collections.Generic;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(384)]
    public class GroupUpdateNotification : WebSocketMessage
    {
        public List<UpdateGroupMemberData> Members;
        public GameType GameType;
        public ushort SubTypeMask;
        public BotDifficulty AllyDifficulty;
        public BotDifficulty EnemyDifficulty;
        public long GroupId;
    }
}
