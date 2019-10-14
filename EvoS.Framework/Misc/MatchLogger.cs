using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("MatchLogger")]
    public class MatchLogger : MonoBehaviour
    {
        public bool m_logMatch;

        public MatchLogger()
        {
        }

        public MatchLogger(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_logMatch = stream.ReadBoolean();
            stream.AlignTo();
        }

        public override string ToString()
        {
            return $"{nameof(MatchLogger)}(" +
                   $"{nameof(m_logMatch)}: {m_logMatch}, " +
                   ")";
        }
    }
}
