using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("AbilityMod_ScoundrelEvasionRoll")]
    public class AbilityMod_ScoundrelEvasionRoll : AbilityMod
    {
        public AbilityModPropertyInt m_extraEnergyPerStepMod;
        public bool m_dropTrapWireOnStart;
        public AbilityGridPattern m_trapwirePattern;
        public StandardBarrierData m_trapWireBarrierData;
        public SerializedComponent m_trapwireCastSequencePrefab;
        public StandardEffectInfo m_additionalEffectOnStart;
        public SerializedComponent m_additionalEffectCastSequencePrefab;
        public int m_techPointGainPerAdjacentAlly;
        public int m_techPointGrantedToAdjacentAllies;
        public StandardEffectInfo m_effectToSelfForLandingInBrush;

        public AbilityMod_ScoundrelEvasionRoll()
        {
        }

        public AbilityMod_ScoundrelEvasionRoll(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            base.DeserializeAsset(assetFile, stream);

            m_extraEnergyPerStepMod = new AbilityModPropertyInt(assetFile, stream);
            m_dropTrapWireOnStart = stream.ReadBoolean();
            stream.AlignTo();
            m_trapwirePattern = (AbilityGridPattern) stream.ReadInt32();
            m_trapWireBarrierData = new StandardBarrierData(assetFile, stream);
            m_trapwireCastSequencePrefab = new SerializedComponent(assetFile, stream);
            m_additionalEffectOnStart = new StandardEffectInfo(assetFile, stream);
            m_additionalEffectCastSequencePrefab = new SerializedComponent(assetFile, stream);
            m_techPointGainPerAdjacentAlly = stream.ReadInt32();
            m_techPointGrantedToAdjacentAllies = stream.ReadInt32();
            m_effectToSelfForLandingInBrush = new StandardEffectInfo(assetFile, stream);
        }

        public override string ToString()
        {
            return $"{nameof(AbilityMod_ScoundrelEvasionRoll)}>(" +
                   $"{nameof(m_extraEnergyPerStepMod)}: {m_extraEnergyPerStepMod}, " +
                   $"{nameof(m_dropTrapWireOnStart)}: {m_dropTrapWireOnStart}, " +
                   $"{nameof(m_trapwirePattern)}: {m_trapwirePattern}, " +
                   $"{nameof(m_trapWireBarrierData)}: {m_trapWireBarrierData}, " +
                   $"{nameof(m_trapwireCastSequencePrefab)}: {m_trapwireCastSequencePrefab}, " +
                   $"{nameof(m_additionalEffectOnStart)}: {m_additionalEffectOnStart}, " +
                   $"{nameof(m_additionalEffectCastSequencePrefab)}: {m_additionalEffectCastSequencePrefab}, " +
                   $"{nameof(m_techPointGainPerAdjacentAlly)}: {m_techPointGainPerAdjacentAlly}, " +
                   $"{nameof(m_techPointGrantedToAdjacentAllies)}: {m_techPointGrantedToAdjacentAllies}, " +
                   $"{nameof(m_effectToSelfForLandingInBrush)}: {m_effectToSelfForLandingInBrush}, " +
                   ")";
        }
    }
}
