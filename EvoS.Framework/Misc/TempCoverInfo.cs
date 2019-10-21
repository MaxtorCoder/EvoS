using EvoS.Framework.Network.NetworkBehaviours;

namespace EvoS.Framework.Misc
{
    public struct TempCoverInfo
    {
        public ActorCover.CoverDirections m_coverDir;
        public bool m_ignoreMinDist;

        public TempCoverInfo(ActorCover.CoverDirections coverDir, bool ignoreMinDist)
        {
            m_coverDir = coverDir;
            m_ignoreMinDist = ignoreMinDist;
        }
    }
}
