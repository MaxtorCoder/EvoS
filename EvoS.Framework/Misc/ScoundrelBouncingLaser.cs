using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized.Behaviours;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("ScoundrelBouncingLaser")]
    public class ScoundrelBouncingLaser : Ability
    {
        public int m_damageAmount;
        public int m_minDamageAmount;
        public int m_damageChangePerHit;
        public int m_bonusDamagePerBounce;
        public float m_width;
        public float m_maxDistancePerBounce;
        public float m_maxTotalDistance;
        public int m_maxBounces;
        public int m_maxTargetsHit;

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            base.DeserializeAsset(assetFile, stream);

            m_damageAmount = stream.ReadInt32(); // int32
            m_minDamageAmount = stream.ReadInt32(); // int32
            m_damageChangePerHit = stream.ReadInt32(); // int32
            m_bonusDamagePerBounce = stream.ReadInt32(); // int32
            m_width = stream.ReadSingle(); // float32
            m_maxDistancePerBounce = stream.ReadSingle(); // float32
            m_maxTotalDistance = stream.ReadSingle(); // float32
            m_maxBounces = stream.ReadInt32(); // int32
            m_maxTargetsHit = stream.ReadInt32(); // int32
        }

        public override string ToString()
        {
            return $"{nameof(ScoundrelBouncingLaser)}>(" +
                   $"{nameof(m_damageAmount)}: {m_damageAmount}, " +
                   $"{nameof(m_minDamageAmount)}: {m_minDamageAmount}, " +
                   $"{nameof(m_damageChangePerHit)}: {m_damageChangePerHit}, " +
                   $"{nameof(m_bonusDamagePerBounce)}: {m_bonusDamagePerBounce}, " +
                   $"{nameof(m_width)}: {m_width}, " +
                   $"{nameof(m_maxDistancePerBounce)}: {m_maxDistancePerBounce}, " +
                   $"{nameof(m_maxTotalDistance)}: {m_maxTotalDistance}, " +
                   $"{nameof(m_maxBounces)}: {m_maxBounces}, " +
                   $"{nameof(m_maxTargetsHit)}: {m_maxTargetsHit}, " +
                   ")";
        }
    }
}
