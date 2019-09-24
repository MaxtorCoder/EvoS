using System;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(389)]
    public struct GroupMemberVisualData
    {
        public CharacterVisualInfo VisualInfo;
        public int ForegroundBannerID;
        public int BackgroundBannerID;
        public int TitleID;
        public int RibbonID;
    }
}
