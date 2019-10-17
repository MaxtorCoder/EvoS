using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("TextRegion")]
    public class TextRegion : ISerializedItem
    {
        public SerializedComponent m_location;
        public string m_text;

        public TextRegion()
        {
        }

        public TextRegion(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_location = new SerializedComponent(assetFile, stream);
            m_text = stream.ReadString32();
        }

        public override string ToString()
        {
            return $"{nameof(TextRegion)}>(" +
                   $"{nameof(m_location)}: {m_location}, " +
                   $"{nameof(m_text)}: {m_text}, " +
                   ")";
        }
    }
}
