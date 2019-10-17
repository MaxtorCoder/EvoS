using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("MinionData")]
    public class MinionData : MonoBehaviour
    {
        public float m_waypointReachedDistance;
        public bool m_movePastEnemyPlayers;

        public MinionData()
        {
        }

        public MinionData(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_waypointReachedDistance = stream.ReadSingle();
            m_movePastEnemyPlayers = stream.ReadBoolean();
            stream.AlignTo();
        }

        public override string ToString()
        {
            return $"{nameof(MinionData)}(" +
                   $"{nameof(m_waypointReachedDistance)}: {m_waypointReachedDistance}, " +
                   $"{nameof(m_movePastEnemyPlayers)}: {m_movePastEnemyPlayers}, " +
                   ")";
        }
    }
}
