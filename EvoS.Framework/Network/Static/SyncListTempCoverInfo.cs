using EvoS.Framework.Misc;
using EvoS.Framework.Network.NetworkBehaviours;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.Static
{
    public class SyncListTempCoverInfo : SyncListStruct<TempCoverInfo>
    {
        public override void SerializeItem(NetworkWriter writer, TempCoverInfo item)
        {
            writer.Write((int) item.m_coverDir);
            writer.Write(item.m_ignoreMinDist);
        }

        public override TempCoverInfo DeserializeItem(NetworkReader reader)
        {
            return new TempCoverInfo()
            {
                m_coverDir = (ActorCover.CoverDirections) reader.ReadInt32(),
                m_ignoreMinDist = reader.ReadBoolean()
            };
        }
    }
}
