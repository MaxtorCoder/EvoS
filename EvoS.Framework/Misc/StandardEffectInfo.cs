using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("StandardEffectInfo")]
    public class StandardEffectInfo : ISerializedItem
    {
        public bool m_applyEffect;
        public StandardActorEffectData m_effectData;

        public StandardEffectInfo()
        {
            
        }
        
        public StandardEffectInfo(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_applyEffect = stream.ReadBoolean(); stream.AlignTo();
            m_effectData = new StandardActorEffectData(assetFile, stream);
        }

        public override string ToString()
        {
            return $"{nameof(StandardEffectInfo)}>(" +
                   $"{nameof(m_applyEffect)}: {m_applyEffect}, " +
                   $"{nameof(m_effectData)}: {m_effectData}, " +
                   ")";
        }
    }
}
