using System;
using System.Collections.Generic;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(6)]
    public class SeasonChapterQuests
    {
        public Dictionary<int, List<int>> m_chapterQuests = new Dictionary<int, List<int>>();
    }
}
