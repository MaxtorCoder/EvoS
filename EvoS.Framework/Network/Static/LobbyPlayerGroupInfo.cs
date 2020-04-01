using System;
using System.Collections.Generic;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(774)]
    public class LobbyPlayerGroupInfo
    {
        public LobbyPlayerGroupInfo()
        {
            SelectedQueueType = GameType.None;
        }

        public LobbyPlayerGroupInfo Clone()
        {
            return (LobbyPlayerGroupInfo) MemberwiseClone();
        }

        public bool InAGroup;
        public bool IsLeader;
        public GameType SelectedQueueType;
        public ushort SubTypeMask;
        private LobbyCharacterInfo m_ChararacterInfo;
        public string MemberDisplayName;
        [EvosMessage(385)]
        public List<UpdateGroupMemberData> Members;
    }
}
