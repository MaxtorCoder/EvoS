using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("AbilityMod_ScoundrelBouncingLaser")]
    public class AbilityMod_ScoundrelBouncingLaser : AbilityMod
    {
        public AbilityModPropertyInt m_maxTargetsMod;
        public AbilityModPropertyInt m_maxBounceMod;
        public AbilityModPropertyFloat m_laserWidthMod;
        public AbilityModPropertyFloat m_distancePerBounceMod;
        public AbilityModPropertyFloat m_maxTotalDistanceMod;
        public AbilityModPropertyInt m_baseDamageMod;
        public AbilityModPropertyInt m_minDamageMod;
        public AbilityModPropertyInt m_damageChangePerHitMod;
        public AbilityModPropertyInt m_bonusDamagePerBounceMod;

        public AbilityMod_ScoundrelBouncingLaser()
        {
        }

        public AbilityMod_ScoundrelBouncingLaser(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            base.DeserializeAsset(assetFile, stream);

            m_maxTargetsMod = new AbilityModPropertyInt(assetFile, stream);
            m_maxBounceMod = new AbilityModPropertyInt(assetFile, stream);
            m_laserWidthMod = new AbilityModPropertyFloat(assetFile, stream);
            m_distancePerBounceMod = new AbilityModPropertyFloat(assetFile, stream);
            m_maxTotalDistanceMod = new AbilityModPropertyFloat(assetFile, stream);
            m_baseDamageMod = new AbilityModPropertyInt(assetFile, stream);
            m_minDamageMod = new AbilityModPropertyInt(assetFile, stream);
            m_damageChangePerHitMod = new AbilityModPropertyInt(assetFile, stream);
            m_bonusDamagePerBounceMod = new AbilityModPropertyInt(assetFile, stream);
        }

        public override string ToString()
        {
            return $"{nameof(AbilityMod_ScoundrelBouncingLaser)}>(" +
                   $"{nameof(m_maxTargetsMod)}: {m_maxTargetsMod}, " +
                   $"{nameof(m_maxBounceMod)}: {m_maxBounceMod}, " +
                   $"{nameof(m_laserWidthMod)}: {m_laserWidthMod}, " +
                   $"{nameof(m_distancePerBounceMod)}: {m_distancePerBounceMod}, " +
                   $"{nameof(m_maxTotalDistanceMod)}: {m_maxTotalDistanceMod}, " +
                   $"{nameof(m_baseDamageMod)}: {m_baseDamageMod}, " +
                   $"{nameof(m_minDamageMod)}: {m_minDamageMod}, " +
                   $"{nameof(m_damageChangePerHitMod)}: {m_damageChangePerHitMod}, " +
                   $"{nameof(m_bonusDamagePerBounceMod)}: {m_bonusDamagePerBounceMod}, " +
                   ")";
        }
    }
}
