using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized.Behaviours;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("ScoundrelEvasionRoll")]
    public class ScoundrelEvasionRoll : Ability
    {
        public int m_extraEnergyPerStep;

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            base.DeserializeAsset(assetFile, stream);

            m_extraEnergyPerStep = stream.ReadInt32(); // int32
        }

        public override string ToString()
        {
            return $"{nameof(ScoundrelEvasionRoll)}>(" +
                   $"{nameof(m_extraEnergyPerStep)}: {m_extraEnergyPerStep}, " +
                   ")";
        }
    }
}
