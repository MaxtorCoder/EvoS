using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized.Behaviours;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("AbilityMod_ScoundrelBlindFire")]
    public class AbilityMod_ScoundrelBlindFire : AbilityMod
    {
        public AbilityModPropertyInt m_damageMod;
        public AbilityModPropertyFloat m_coneWidthAngleMod;
        public AbilityModPropertyBool m_penetrateLineOfSight;
        public StandardEffectInfo m_effectOnTargetsHit;

        public AbilityMod_ScoundrelBlindFire()
        {
            
        }
        
        public AbilityMod_ScoundrelBlindFire(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            base.DeserializeAsset(assetFile, stream);

            m_damageMod = new AbilityModPropertyInt(assetFile, stream); // class AbilityModPropertyInt
            m_coneWidthAngleMod = new AbilityModPropertyFloat(assetFile, stream); // class AbilityModPropertyFloat
            m_penetrateLineOfSight = new AbilityModPropertyBool(assetFile, stream); // class AbilityModPropertyBool
            m_effectOnTargetsHit = new StandardEffectInfo(assetFile, stream); // class StandardEffectInfo
        }

        public override string ToString()
        {
            return $"{nameof(AbilityMod_ScoundrelBlindFire)}>(" +
                   $"{nameof(m_damageMod)}: {m_damageMod}, " +
                   $"{nameof(m_coneWidthAngleMod)}: {m_coneWidthAngleMod}, " +
                   $"{nameof(m_penetrateLineOfSight)}: {m_penetrateLineOfSight}, " +
                   $"{nameof(m_effectOnTargetsHit)}: {m_effectOnTargetsHit}, " +
                   ")";
        }
    }
}
