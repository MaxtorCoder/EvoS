using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("ChainAbility_ShootNearestActor")]
    public class ChainAbility_ShootNearestActor : Ability
    {
        public int m_enemyDamageAmount;
        public StandardEffectInfo m_enemyHitEffect;
        public int m_allyHealAmount;
        public StandardEffectInfo m_allyHitEffect;
        public float m_maxRange;
        public bool m_includeAllies;
        public bool m_includeEnemies;
        public bool m_penetrateLos;
        public SerializedComponent m_castSequencePrefab;

        public ChainAbility_ShootNearestActor()
        {
        }

        public ChainAbility_ShootNearestActor(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            base.DeserializeAsset(assetFile, stream);

            m_enemyDamageAmount = stream.ReadInt32();
            m_enemyHitEffect = new StandardEffectInfo(assetFile, stream);
            m_allyHealAmount = stream.ReadInt32();
            m_allyHitEffect = new StandardEffectInfo(assetFile, stream);
            m_maxRange = stream.ReadSingle();
            m_includeAllies = stream.ReadBoolean();
            stream.AlignTo();
            m_includeEnemies = stream.ReadBoolean();
            stream.AlignTo();
            m_penetrateLos = stream.ReadBoolean();
            stream.AlignTo();
            m_castSequencePrefab = new SerializedComponent(assetFile, stream);
        }

        public override string ToString()
        {
            return $"{nameof(ChainAbility_ShootNearestActor)}>(" +
                   $"{nameof(m_enemyDamageAmount)}: {m_enemyDamageAmount}, " +
                   $"{nameof(m_enemyHitEffect)}: {m_enemyHitEffect}, " +
                   $"{nameof(m_allyHealAmount)}: {m_allyHealAmount}, " +
                   $"{nameof(m_allyHitEffect)}: {m_allyHitEffect}, " +
                   $"{nameof(m_maxRange)}: {m_maxRange}, " +
                   $"{nameof(m_includeAllies)}: {m_includeAllies}, " +
                   $"{nameof(m_includeEnemies)}: {m_includeEnemies}, " +
                   $"{nameof(m_penetrateLos)}: {m_penetrateLos}, " +
                   $"{nameof(m_castSequencePrefab)}: {m_castSequencePrefab}, " +
                   ")";
        }
    }
}
