using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("AbilityMod_ScoundrelRunAndGun")]
    public class AbilityMod_ScoundrelRunAndGun : AbilityMod
    {
        public AbilityModPropertyInt m_damageMod;
        public AbilityModPropertyInt m_techPointGainWithNoHits;
        public int m_numTargeters;
        public float m_minDistanceBetweenSteps;
        public float m_maxDistanceBetweenSteps;
        public float m_minDistanceBetweenAnySteps;

        public AbilityMod_ScoundrelRunAndGun()
        {
            
        }
        
        public AbilityMod_ScoundrelRunAndGun(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            base.DeserializeAsset(assetFile, stream);

            m_damageMod = new AbilityModPropertyInt(assetFile, stream);
            m_techPointGainWithNoHits = new AbilityModPropertyInt(assetFile, stream);
            m_numTargeters = stream.ReadInt32();
            m_minDistanceBetweenSteps = stream.ReadSingle();
            m_maxDistanceBetweenSteps = stream.ReadSingle();
            m_minDistanceBetweenAnySteps = stream.ReadSingle();
        }

        public override string ToString()
        {
            return $"{nameof(AbilityMod_ScoundrelRunAndGun)}>(" +
                   $"{nameof(m_damageMod)}: {m_damageMod}, " +
                   $"{nameof(m_techPointGainWithNoHits)}: {m_techPointGainWithNoHits}, " +
                   $"{nameof(m_numTargeters)}: {m_numTargeters}, " +
                   $"{nameof(m_minDistanceBetweenSteps)}: {m_minDistanceBetweenSteps}, " +
                   $"{nameof(m_maxDistanceBetweenSteps)}: {m_maxDistanceBetweenSteps}, " +
                   $"{nameof(m_minDistanceBetweenAnySteps)}: {m_minDistanceBetweenAnySteps}, " +
                   ")";
        }
    }
}

