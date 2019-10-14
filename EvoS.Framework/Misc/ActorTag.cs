using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("ActorTag")]
    public class ActorTag : ISerializedItem
    {
        public SerializedVector<string> m_tags;

        public ActorTag()
        {
            
        }
        
        public ActorTag(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_tags = new SerializedVector<string>(assetFile, stream);
        }

        public override string ToString()
        {
            return $"{nameof(ActorTag)}>(" +
                   $"{nameof(m_tags)}: {m_tags}, " +
                   ")";
        }
    }
}
