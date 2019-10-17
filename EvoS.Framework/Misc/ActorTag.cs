using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("ActorTag")]
    public class ActorTag : MonoBehaviour
    {
        public SerializedVector<string> m_tags;

        public ActorTag()
        {
        }

        public ActorTag(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public bool HasTag(string tag)
        {
            return m_tags.Contains(tag);
        }

        public void AddTag(string tag)
        {
            m_tags.Add(tag);
        }

        public void RemoveTag(string tag)
        {
            m_tags.Remove(tag);
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
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
