using System;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(387)]
    public class UpdateGroupMemberData
    {
        public CharacterVisualInfo VisualInfo => VisualData?.VisualInfo ?? new CharacterVisualInfo(0, 0, 0);

        public int ForegroundBannerID => VisualData?.ForegroundBannerID ?? 0;

        public int BackgroundBannerID => VisualData?.BackgroundBannerID ?? 0;

        public int TitleID => VisualData?.TitleID ?? 0;

        public int RibbonID => VisualData?.RibbonID ?? 0;

        public string MemberDisplayName;
        public string MemberHandle;
        public bool HasFullAccess;
        public bool IsLeader;
        public bool IsReady;
        public bool IsInGame;
        public long CreateGameTimestamp;
        public long AccountID;
        public CharacterType MemberDisplayCharacter;
        [EvosMessage(388)]
        public GroupMemberVisualData? VisualData;
        public DateTime? PenaltyTimeout;
        public float GameLeavingPoints;
    }
}
