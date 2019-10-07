using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("AbilityMod_ScoundrelTrapWire")]
    public class AbilityMod_ScoundrelTrapWire : AbilityMod
    {
        public AbilityModPropertyFloat m_barrierScaleMod;
        public bool m_useEnemyMovedThroughOverride;
        public GameplayResponseForActor m_enemyMovedThroughOverride;
        public bool m_useAllyMovedThroughOverride;
        public GameplayResponseForActor m_allyMovedThroughOverride;
        public AbilityModPropertyBarrierDataV2 m_barrierDataMod;
        public AbilityModCooldownReduction m_cooldownReductionsWhenNoHits;
        public SerializedVector<SerializedGameObject> m_barrierSequence;
        public bool m_barrierPlaySequenceOnAllBarriers;

        public AbilityMod_ScoundrelTrapWire()
        {
            
        }
        
        public AbilityMod_ScoundrelTrapWire(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            base.DeserializeAsset(assetFile, stream);

            m_barrierScaleMod = new AbilityModPropertyFloat(assetFile, stream);
            m_useEnemyMovedThroughOverride = stream.ReadBoolean(); stream.AlignTo();
            m_enemyMovedThroughOverride = new GameplayResponseForActor(assetFile, stream);
            m_useAllyMovedThroughOverride = stream.ReadBoolean(); stream.AlignTo();
            m_allyMovedThroughOverride = new GameplayResponseForActor(assetFile, stream);
            m_barrierDataMod = new AbilityModPropertyBarrierDataV2(assetFile, stream);
            m_cooldownReductionsWhenNoHits = new AbilityModCooldownReduction(assetFile, stream);
            m_barrierSequence = new SerializedVector<SerializedGameObject>(assetFile, stream);
            m_barrierPlaySequenceOnAllBarriers = stream.ReadBoolean(); stream.AlignTo();
        }

        public override string ToString()
        {
            return $"{nameof(AbilityMod_ScoundrelTrapWire)}>(" +
                   $"{nameof(m_barrierScaleMod)}: {m_barrierScaleMod}, " +
                   $"{nameof(m_useEnemyMovedThroughOverride)}: {m_useEnemyMovedThroughOverride}, " +
                   $"{nameof(m_enemyMovedThroughOverride)}: {m_enemyMovedThroughOverride}, " +
                   $"{nameof(m_useAllyMovedThroughOverride)}: {m_useAllyMovedThroughOverride}, " +
                   $"{nameof(m_allyMovedThroughOverride)}: {m_allyMovedThroughOverride}, " +
                   $"{nameof(m_barrierDataMod)}: {m_barrierDataMod}, " +
                   $"{nameof(m_cooldownReductionsWhenNoHits)}: {m_cooldownReductionsWhenNoHits}, " +
                   $"{nameof(m_barrierSequence)}: {m_barrierSequence}, " +
                   $"{nameof(m_barrierPlaySequenceOnAllBarriers)}: {m_barrierPlaySequenceOnAllBarriers}, " +
                   ")";
        }
    }
}

