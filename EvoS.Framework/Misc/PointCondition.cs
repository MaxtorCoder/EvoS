using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("PointCondition")]
    public class PointCondition : ISerializedItem
    {
        public PointRelationship m_pointsMustBe;
        public int threshold;
        public bool subtractEnemyPoints;
        public WhenRelationship whenRelationship;
        public SerializedArray<CaptureTheFlag.CTF_VictoryCondition> m_CTF_conditions;
        public SerializedArray<CollectTheCoins.CollectTheCoins_VictoryCondition> m_CTC_conditions;

        public PointCondition()
        {
        }

        public PointCondition(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_pointsMustBe = (PointRelationship) stream.ReadInt32();
            threshold = stream.ReadInt32();
            subtractEnemyPoints = stream.ReadBoolean();
            stream.AlignTo();
            whenRelationship = (WhenRelationship) stream.ReadInt32();
            m_CTF_conditions = new SerializedArray<CaptureTheFlag.CTF_VictoryCondition>(assetFile, stream);
            m_CTC_conditions = new SerializedArray<CollectTheCoins.CollectTheCoins_VictoryCondition>(assetFile, stream);
        }

        public override string ToString()
        {
            return $"{nameof(PointCondition)}>(" +
                   $"{nameof(m_pointsMustBe)}: {m_pointsMustBe}, " +
                   $"{nameof(threshold)}: {threshold}, " +
                   $"{nameof(subtractEnemyPoints)}: {subtractEnemyPoints}, " +
                   $"{nameof(whenRelationship)}: {whenRelationship}, " +
                   $"{nameof(m_CTF_conditions)}: {m_CTF_conditions}, " +
                   $"{nameof(m_CTC_conditions)}: {m_CTC_conditions}, " +
                   ")";
        }

        public enum PointRelationship
        {
            GreaterThan,
            LessThan,
            GreaterThanOrEqualTo,
            LessThanOrEqualTo,
            EqualTo,
            DontCare
        }

        public enum WhenRelationship
        {
            AllTheTime,
            OnlyAfterTurnLimit,
            OnlyBeforeTurnLimit
        }
    }
}
