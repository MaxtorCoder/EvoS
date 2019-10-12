using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("BarrierResponseOnShot")]
    public class BarrierResponseOnShot : ISerializedItem
    {
        public SerializedComponent m_onShotSequencePrefab;
        public bool m_useShooterPosAsReactionSequenceTargetPos;
        public int m_healOnOwnerFromEnemyShot;
        public int m_energyGainOnOwnerFromEnemyShot;
        public StandardEffectInfo m_effectOnOwnerFromEnemyShot;
        public int m_damageOnEnemyOnShot;
        public int m_energyLossOnEnemyOnShot;
        public StandardEffectInfo m_effectOnEnemyOnShot;

        public BarrierResponseOnShot()
        {
        }

        public BarrierResponseOnShot(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_onShotSequencePrefab = new SerializedComponent(assetFile, stream);
            m_useShooterPosAsReactionSequenceTargetPos = stream.ReadBoolean();
            stream.AlignTo();
            m_healOnOwnerFromEnemyShot = stream.ReadInt32();
            m_energyGainOnOwnerFromEnemyShot = stream.ReadInt32();
            m_effectOnOwnerFromEnemyShot = new StandardEffectInfo(assetFile, stream);
            m_damageOnEnemyOnShot = stream.ReadInt32();
            m_energyLossOnEnemyOnShot = stream.ReadInt32();
            m_effectOnEnemyOnShot = new StandardEffectInfo(assetFile, stream);
        }

        public override string ToString()
        {
            return $"{nameof(BarrierResponseOnShot)}>(" +
                   $"{nameof(m_onShotSequencePrefab)}: {m_onShotSequencePrefab}, " +
                   $"{nameof(m_useShooterPosAsReactionSequenceTargetPos)}: {m_useShooterPosAsReactionSequenceTargetPos}, " +
                   $"{nameof(m_healOnOwnerFromEnemyShot)}: {m_healOnOwnerFromEnemyShot}, " +
                   $"{nameof(m_energyGainOnOwnerFromEnemyShot)}: {m_energyGainOnOwnerFromEnemyShot}, " +
                   $"{nameof(m_effectOnOwnerFromEnemyShot)}: {m_effectOnOwnerFromEnemyShot}, " +
                   $"{nameof(m_damageOnEnemyOnShot)}: {m_damageOnEnemyOnShot}, " +
                   $"{nameof(m_energyLossOnEnemyOnShot)}: {m_energyLossOnEnemyOnShot}, " +
                   $"{nameof(m_effectOnEnemyOnShot)}: {m_effectOnEnemyOnShot}, " +
                   ")";
        }
    }
}
