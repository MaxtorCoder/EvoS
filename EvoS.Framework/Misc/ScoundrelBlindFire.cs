using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized.Behaviours;

namespace EvoS.Framework.Misc
{
    [SerializedMonoBehaviour("ScoundrelBlindFire")]
    public class ScoundrelBlindFire : Ability
    {
        public float m_coneWidthAngle = 90f;
        public float m_coneLength = 10f;
        public int m_damageAmount = 20;
        public int m_maxTargets = 2;
        public bool m_includeTargetsInCover = true;
        public float m_coneBackwardOffset;
        public bool m_penetrateLineOfSight;
        public bool m_restrictWithinCover;
//        private AbilityMod_ScoundrelBlindFire m_abilityMod;

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            base.DeserializeAsset(assetFile, stream);

            m_coneWidthAngle = stream.ReadSingle();
            m_coneLength = stream.ReadSingle();
            m_damageAmount = stream.ReadInt32();
            m_maxTargets = stream.ReadInt32();
            m_includeTargetsInCover = stream.ReadBoolean();
            stream.AlignTo();
            m_coneBackwardOffset = stream.ReadSingle();
            m_penetrateLineOfSight = stream.ReadBoolean();
            stream.AlignTo();
            m_restrictWithinCover = stream.ReadBoolean();
            stream.AlignTo();
        }
    }
}
