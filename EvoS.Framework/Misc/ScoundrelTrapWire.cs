using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("ScoundrelTrapWire")]
    public class ScoundrelTrapWire : Ability
    {
        public AbilityGridPattern m_pattern;
        public float m_barrierSizeScale;
        public StandardBarrierData m_barrierData;

        public ScoundrelTrapWire()
        {
        }

        public ScoundrelTrapWire(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            base.DeserializeAsset(assetFile, stream);

            m_pattern = (AbilityGridPattern) stream.ReadInt32(); // valuetype AbilityGridPattern
            m_barrierSizeScale = stream.ReadSingle(); // float32
            m_barrierData = new StandardBarrierData(assetFile, stream); // class StandardBarrierData
        }

        public override string ToString()
        {
            return $"{nameof(ScoundrelTrapWire)}>(" +
                   $"{nameof(m_pattern)}: {m_pattern}, " +
                   $"{nameof(m_barrierSizeScale)}: {m_barrierSizeScale}, " +
                   $"{nameof(m_barrierData)}: {m_barrierData}, " +
                   ")";
        }
    }
}
