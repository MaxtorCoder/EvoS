using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("StandardBarrierData")]
    public class StandardBarrierData : ISerializedItem
    {
        public float m_width;
        public bool m_bidirectional;
        public BlockingRules m_blocksVision;
        public BlockingRules m_blocksAbilities;
        public BlockingRules m_blocksMovement;
        public BlockingRules m_blocksMovementOnCrossover;
        public BlockingRules m_blocksPositionTargeting;
        public bool m_considerAsCover;
        public int m_maxDuration;
        public SerializedVector<SerializedGameObject> m_barrierSequencePrefabs;
        public GameplayResponseForActor m_onEnemyMovedThrough;
        public GameplayResponseForActor m_onAllyMovedThrough;
        public bool m_removeAtTurnEndIfEnemyMovedThrough;
        public bool m_removeAtTurnEndIfAllyMovedThrough;
        public bool m_removeAtPhaseEndIfEnemyMovedThrough;
        public bool m_removeAtPhaseEndIfAllyMovedThrough;
        public int m_maxHits;
        public bool m_endOnCasterDeath;
        public BarrierResponseOnShot m_responseOnShotBlock;

        public StandardBarrierData()
        {
            
        }
        
        public StandardBarrierData(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_width = stream.ReadSingle(); // float32
            m_bidirectional = stream.ReadBoolean(); stream.AlignTo(); // bool
            m_blocksVision = (BlockingRules) stream.ReadInt32(); // valuetype BlockingRules
            m_blocksAbilities = (BlockingRules) stream.ReadInt32(); // valuetype BlockingRules
            m_blocksMovement = (BlockingRules) stream.ReadInt32(); // valuetype BlockingRules
            m_blocksMovementOnCrossover = (BlockingRules) stream.ReadInt32(); // valuetype BlockingRules
            m_blocksPositionTargeting = (BlockingRules) stream.ReadInt32(); // valuetype BlockingRules
            m_considerAsCover = stream.ReadBoolean(); stream.AlignTo(); // bool
            m_maxDuration = stream.ReadInt32(); // int32
            m_barrierSequencePrefabs = new SerializedVector<SerializedGameObject>(assetFile, stream); // class [mscorlib]System.Collections.Generic.List`1<class [UnityEngine]UnityEngine.GameObject>
            m_onEnemyMovedThrough = new GameplayResponseForActor(assetFile, stream); // class GameplayResponseForActor
            m_onAllyMovedThrough = new GameplayResponseForActor(assetFile, stream); // class GameplayResponseForActor
            m_removeAtTurnEndIfEnemyMovedThrough = stream.ReadBoolean(); stream.AlignTo(); // bool
            m_removeAtTurnEndIfAllyMovedThrough = stream.ReadBoolean(); stream.AlignTo(); // bool
            m_removeAtPhaseEndIfEnemyMovedThrough = stream.ReadBoolean(); stream.AlignTo(); // bool
            m_removeAtPhaseEndIfAllyMovedThrough = stream.ReadBoolean(); stream.AlignTo(); // bool
            m_maxHits = stream.ReadInt32(); // int32
            m_endOnCasterDeath = stream.ReadBoolean(); stream.AlignTo(); // bool
            m_responseOnShotBlock = new BarrierResponseOnShot(assetFile, stream); // class BarrierResponseOnShot
        }

        public override string ToString()
        {
            return $"{nameof(StandardBarrierData)}>(" +
                   $"{nameof(m_width)}: {m_width}, " +
                   $"{nameof(m_bidirectional)}: {m_bidirectional}, " +
                   $"{nameof(m_blocksVision)}: {m_blocksVision}, " +
                   $"{nameof(m_blocksAbilities)}: {m_blocksAbilities}, " +
                   $"{nameof(m_blocksMovement)}: {m_blocksMovement}, " +
                   $"{nameof(m_blocksMovementOnCrossover)}: {m_blocksMovementOnCrossover}, " +
                   $"{nameof(m_blocksPositionTargeting)}: {m_blocksPositionTargeting}, " +
                   $"{nameof(m_considerAsCover)}: {m_considerAsCover}, " +
                   $"{nameof(m_maxDuration)}: {m_maxDuration}, " +
                   $"{nameof(m_barrierSequencePrefabs)}: {m_barrierSequencePrefabs}, " +
                   $"{nameof(m_onEnemyMovedThrough)}: {m_onEnemyMovedThrough}, " +
                   $"{nameof(m_onAllyMovedThrough)}: {m_onAllyMovedThrough}, " +
                   $"{nameof(m_removeAtTurnEndIfEnemyMovedThrough)}: {m_removeAtTurnEndIfEnemyMovedThrough}, " +
                   $"{nameof(m_removeAtTurnEndIfAllyMovedThrough)}: {m_removeAtTurnEndIfAllyMovedThrough}, " +
                   $"{nameof(m_removeAtPhaseEndIfEnemyMovedThrough)}: {m_removeAtPhaseEndIfEnemyMovedThrough}, " +
                   $"{nameof(m_removeAtPhaseEndIfAllyMovedThrough)}: {m_removeAtPhaseEndIfAllyMovedThrough}, " +
                   $"{nameof(m_maxHits)}: {m_maxHits}, " +
                   $"{nameof(m_endOnCasterDeath)}: {m_endOnCasterDeath}, " +
                   $"{nameof(m_responseOnShotBlock)}: {m_responseOnShotBlock}, " +
                   ")";
        }
    }
}

