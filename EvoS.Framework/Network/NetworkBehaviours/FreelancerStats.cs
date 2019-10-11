using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.NetworkBehaviours
{
    [Serializable]
    [SerializedMonoBehaviour("FreelancerStats")]
    public class FreelancerStats : NetworkBehaviour
    {
        public bool m_ignoreForLocalization;
        public SerializedVector<string> m_name;
        public SerializedVector<string> m_descriptions;

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_ignoreForLocalization = stream.ReadBoolean(); stream.AlignTo(); // bool
            m_name = new SerializedVector<string>(assetFile, stream); // class [mscorlib]System.Collections.Generic.List`1<string>
            m_descriptions = new SerializedVector<string>(assetFile, stream); // class [mscorlib]System.Collections.Generic.List`1<string>
        }

        public override string ToString()
        {
            return $"{nameof(FreelancerStats)}>(" +
                   $"{nameof(m_ignoreForLocalization)}: {m_ignoreForLocalization}, " +
                   $"{nameof(m_name)}: {m_name}, " +
                   $"{nameof(m_descriptions)}: {m_descriptions}, " +
                   ")";
        }
    }
}
