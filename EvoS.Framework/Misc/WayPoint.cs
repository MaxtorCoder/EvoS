using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("WayPoint")]
    public class WayPoint : MonoBehaviour
    {
        public int TurnsToDelay;
        public bool MustArriveAtWayPointToContinue;

        public WayPoint()
        {
        }

        public WayPoint(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            TurnsToDelay = stream.ReadInt32();
            MustArriveAtWayPointToContinue = stream.ReadBoolean();
            stream.AlignTo();
        }

        public override string ToString()
        {
            return $"{nameof(WayPoint)}(" +
                   $"{nameof(TurnsToDelay)}: {TurnsToDelay}, " +
                   $"{nameof(MustArriveAtWayPointToContinue)}: {MustArriveAtWayPointToContinue}, " +
                   ")";
        }
    }
}
