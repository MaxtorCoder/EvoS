using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("SpoilsManager")]
    public class SpoilsManager : ISerializedItem
    {
        public SerializedArray<SerializedComponent> m_heroSpoils;
        public SerializedArray<SerializedComponent> m_minionSpoils;

        public SpoilsManager()
        {
        }

        public SpoilsManager(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_heroSpoils = new SerializedArray<SerializedComponent>(assetFile, stream);
            m_minionSpoils = new SerializedArray<SerializedComponent>(assetFile, stream);
        }

        public override string ToString()
        {
            return $"{nameof(SpoilsManager)}>(" +
                   $"{nameof(m_heroSpoils)}: {m_heroSpoils}, " +
                   $"{nameof(m_minionSpoils)}: {m_minionSpoils}, " +
                   ")";
        }

        public enum SpoilsType
        {
            None,
            Hero,
            Minion,
        }
    }
}
