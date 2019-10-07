using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Misc
{
    [Serializable]
    public class StandardActorEffectData
    {
        public string m_effectName;
        public int m_duration;
        public int m_maxStackSize;
        public int m_damagePerTurn;
        public int m_healingPerTurn;
        public HealingType m_healingType = HealingType.Effect;
        public int m_perTurnHitDelayTurns;
        public int m_absorbAmount;
        public int m_nextTurnAbsorbAmount;
        public bool m_dontEndEarlyOnShieldDeplete;
        public int m_damagePerMoveSquare;
        public int m_healPerMoveSquare;
        public int m_techPointLossPerMoveSquare;
        public int m_techPointGainPerMoveSquare;
        public int m_techPointChangeOnStart;
        public int m_techPointGainPerTurn;
        public int m_techPointLossPerTurn;
        public InvisibilityBreakMode m_invisBreakMode;
        public bool m_removeInvisibilityOnLastResolveStart;
        public bool m_removeRevealedOnLastResolveStart;
        public SerializedArray<AbilityStatMod> m_statMods;
        public SerializedArray<StatusType> m_statusChanges;
        public StatusDelayMode m_statusDelayMode;
        public SerializedArray<EffectEndTag> m_endTriggers;
        public SerializedVector<SerializedComponent> m_sequencePrefabs;
        public SerializedComponent m_tickSequencePrefab;

        public StandardActorEffectData()
        {
        }

        public StandardActorEffectData(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_effectName = stream.ReadString32();
            m_duration = stream.ReadInt32();
            m_maxStackSize = stream.ReadInt32();
            m_damagePerTurn = stream.ReadInt32();
            m_healingPerTurn = stream.ReadInt32();
            m_healingType = (HealingType) stream.ReadInt32();
            m_perTurnHitDelayTurns = stream.ReadInt32();
            m_absorbAmount = stream.ReadInt32();
            m_nextTurnAbsorbAmount = stream.ReadInt32();
            m_dontEndEarlyOnShieldDeplete = stream.ReadBoolean();
            stream.AlignTo();
            m_damagePerMoveSquare = stream.ReadInt32();
            m_healPerMoveSquare = stream.ReadInt32();
            m_techPointLossPerMoveSquare = stream.ReadInt32();
            m_techPointGainPerMoveSquare = stream.ReadInt32();
            m_techPointChangeOnStart = stream.ReadInt32();
            m_techPointGainPerTurn = stream.ReadInt32();
            m_techPointLossPerTurn = stream.ReadInt32();
            m_invisBreakMode = (InvisibilityBreakMode) stream.ReadInt32();
            m_removeInvisibilityOnLastResolveStart = stream.ReadBoolean();
            stream.AlignTo();
            m_removeRevealedOnLastResolveStart = stream.ReadBoolean();
            stream.AlignTo();
            m_statMods = new SerializedArray<AbilityStatMod>(assetFile, stream);
            m_statusChanges = new SerializedArray<StatusType>(assetFile, stream);
            m_statusDelayMode = (StatusDelayMode) stream.ReadInt32();
            m_endTriggers = new SerializedArray<EffectEndTag>(assetFile, stream);
            m_sequencePrefabs = new SerializedVector<SerializedComponent>(assetFile, stream);
            m_tickSequencePrefab = new SerializedComponent(assetFile, stream);
        }

        public override string ToString()
        {
            return $"{nameof(StandardActorEffectData)}>(" +
                   $"{nameof(m_effectName)}: {m_effectName}, " +
                   $"{nameof(m_duration)}: {m_duration}, " +
                   $"{nameof(m_maxStackSize)}: {m_maxStackSize}, " +
                   $"{nameof(m_damagePerTurn)}: {m_damagePerTurn}, " +
                   $"{nameof(m_healingPerTurn)}: {m_healingPerTurn}, " +
                   $"{nameof(m_healingType)}: {m_healingType}, " +
                   $"{nameof(m_perTurnHitDelayTurns)}: {m_perTurnHitDelayTurns}, " +
                   $"{nameof(m_absorbAmount)}: {m_absorbAmount}, " +
                   $"{nameof(m_nextTurnAbsorbAmount)}: {m_nextTurnAbsorbAmount}, " +
                   $"{nameof(m_dontEndEarlyOnShieldDeplete)}: {m_dontEndEarlyOnShieldDeplete}, " +
                   $"{nameof(m_damagePerMoveSquare)}: {m_damagePerMoveSquare}, " +
                   $"{nameof(m_healPerMoveSquare)}: {m_healPerMoveSquare}, " +
                   $"{nameof(m_techPointLossPerMoveSquare)}: {m_techPointLossPerMoveSquare}, " +
                   $"{nameof(m_techPointGainPerMoveSquare)}: {m_techPointGainPerMoveSquare}, " +
                   $"{nameof(m_techPointChangeOnStart)}: {m_techPointChangeOnStart}, " +
                   $"{nameof(m_techPointGainPerTurn)}: {m_techPointGainPerTurn}, " +
                   $"{nameof(m_techPointLossPerTurn)}: {m_techPointLossPerTurn}, " +
                   $"{nameof(m_invisBreakMode)}: {m_invisBreakMode}, " +
                   $"{nameof(m_removeInvisibilityOnLastResolveStart)}: {m_removeInvisibilityOnLastResolveStart}, " +
                   $"{nameof(m_removeRevealedOnLastResolveStart)}: {m_removeRevealedOnLastResolveStart}, " +
                   $"{nameof(m_statMods)}: {m_statMods}, " +
                   $"{nameof(m_statusChanges)}: {m_statusChanges}, " +
                   $"{nameof(m_statusDelayMode)}: {m_statusDelayMode}, " +
                   $"{nameof(m_endTriggers)}: {m_endTriggers}, " +
                   $"{nameof(m_sequencePrefabs)}: {m_sequencePrefabs}, " +
                   $"{nameof(m_tickSequencePrefab)}: {m_tickSequencePrefab}, " +
                   ")";
        }

        public enum InvisibilityBreakMode
        {
            RemoveInvisAndEndEarly,
            SuppressOnly
        }

        public enum StatusDelayMode
        {
            DefaultBehavior,
            AllStatusesDelayToTurnStart,
            NoStatusesDelayToTurnStart
        }
    }
}
