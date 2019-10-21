using System;
using System.Collections.Generic;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Network.NetworkBehaviours;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("AbilityModCooldownReduction")]
    public class AbilityModCooldownReduction
    {
        public AbilityData.ActionType m_onAbility = AbilityData.ActionType.INVALID_ACTION;
        public List<AbilityData.ActionType> m_additionalAbilities = new List<AbilityData.ActionType>();

        public int m_maxReduction = -1;

//  [Header("[Stock/Charge Refresh]")]
        public List<AbilityData.ActionType> m_stockAbilities = new List<AbilityData.ActionType>();

        public ModAmountType m_modAmountType;

//  [Header("[Flat => baseVal + finalAdd], [Mult => Round(baseVal*numTargets) + finalAdd]")]
        public float m_baseValue;
        public float m_finalAdd;
        public int m_minReduction;
        public int m_stockBaseValue;
        public int m_stockFinalAdd;
        public int m_refreshProgressBaseValue;
        public int m_refreshProgressFinalAdd;
        public bool m_resetRefreshProgress;


        public AbilityModCooldownReduction()
        {
        }

        public AbilityModCooldownReduction(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_onAbility = (AbilityData.ActionType) stream.ReadInt32();
            m_additionalAbilities =
                new SerializedVector<AbilityData.ActionType >
                (assetFile,
                    stream); // class [mscorlib]System.Collections.Generic.List`1<valuetype AbilityData/ActionType>
            m_modAmountType = (ModAmountType) stream.ReadInt32();
            m_baseValue = stream.ReadSingle();
            m_finalAdd = stream.ReadSingle();
            m_minReduction = stream.ReadInt32();
            m_maxReduction = stream.ReadInt32();
            m_stockAbilities =
                new SerializedVector<AbilityData.ActionType >
                (assetFile,
                    stream); // class [mscorlib]System.Collections.Generic.List`1<valuetype AbilityData/ActionType>
            m_stockBaseValue = stream.ReadInt32();
            m_stockFinalAdd = stream.ReadInt32();
            m_refreshProgressBaseValue = stream.ReadInt32();
            m_refreshProgressFinalAdd = stream.ReadInt32();
            m_resetRefreshProgress = stream.ReadBoolean();
            stream.AlignTo();
        }

        public override string ToString()
        {
            return $"{nameof(AbilityModCooldownReduction)}>(" +
                   $"{nameof(m_onAbility)}: {m_onAbility}, " +
                   $"{nameof(m_additionalAbilities)}: {m_additionalAbilities}, " +
                   $"{nameof(m_modAmountType)}: {m_modAmountType}, " +
                   $"{nameof(m_baseValue)}: {m_baseValue}, " +
                   $"{nameof(m_finalAdd)}: {m_finalAdd}, " +
                   $"{nameof(m_minReduction)}: {m_minReduction}, " +
                   $"{nameof(m_maxReduction)}: {m_maxReduction}, " +
                   $"{nameof(m_stockAbilities)}: {m_stockAbilities}, " +
                   $"{nameof(m_stockBaseValue)}: {m_stockBaseValue}, " +
                   $"{nameof(m_stockFinalAdd)}: {m_stockFinalAdd}, " +
                   $"{nameof(m_refreshProgressBaseValue)}: {m_refreshProgressBaseValue}, " +
                   $"{nameof(m_refreshProgressFinalAdd)}: {m_refreshProgressFinalAdd}, " +
                   $"{nameof(m_resetRefreshProgress)}: {m_resetRefreshProgress}, " +
                   ")";
        }

        public enum ModAmountType
        {
            FlatOnCast,
            FlatOnAnyNonSelfHit,
            FlatOnAnyAllyHit,
            FlatOnAnyEnemyHit,
            MultPerAllyHit,
            MultPerEnemyHit,
            FlatOnNoEnemyHit,
            FlatOnNoAllyHit,
            FlatOnNoNonCasterHit
        }
    }
}
