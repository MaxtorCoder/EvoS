using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized.Behaviours;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("ScoundrelRunAndGun")]
    public class ScoundrelRunAndGun : Ability
    {
        public int m_damageAmount;
        public float m_damageRadius;
        public bool m_penetrateLineOfSight;
        public bool m_energyRefundAffectedByBuff;

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            base.DeserializeAsset(assetFile, stream);

            m_damageAmount = stream.ReadInt32(); // int32
            m_damageRadius = stream.ReadSingle(); // float32
            m_penetrateLineOfSight = stream.ReadBoolean();
            stream.AlignTo(); // bool
            m_energyRefundAffectedByBuff = stream.ReadBoolean();
            stream.AlignTo(); // bool
        }

        public override string ToString()
        {
            return $"{nameof(ScoundrelRunAndGun)}>(" +
                   $"{nameof(m_damageAmount)}: {m_damageAmount}, " +
                   $"{nameof(m_damageRadius)}: {m_damageRadius}, " +
                   $"{nameof(m_penetrateLineOfSight)}: {m_penetrateLineOfSight}, " +
                   $"{nameof(m_energyRefundAffectedByBuff)}: {m_energyRefundAffectedByBuff}, " +
                   ")";
        }
    }
}
