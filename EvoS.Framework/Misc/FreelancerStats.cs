using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("FreelancerStats")]
    public class FreelancerStats : ISerializedItem
    {
        public bool m_ignoreForLocalization;
        public SerializedVector<string> m_name;
        public SerializedVector<string> m_descriptions;

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
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
