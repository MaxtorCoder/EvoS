using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("TargeterTemplateSwapData")]
    public class TargeterTemplateSwapData : ISerializedItem
    {
        public string m_notes;
        public TargeterTemplateType m_templateToReplace;
        public SerializedComponent m_prefabToUse;

        public TargeterTemplateSwapData()
        {
        }

        public TargeterTemplateSwapData(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_notes = stream.ReadString32();
            m_templateToReplace = (TargeterTemplateType) stream.ReadInt32();
            m_prefabToUse = new SerializedComponent(assetFile, stream);
        }

        public override string ToString()
        {
            return $"{nameof(TargeterTemplateSwapData)}>(" +
                   $"{nameof(m_notes)}: {m_notes}, " +
                   $"{nameof(m_templateToReplace)}: {m_templateToReplace}, " +
                   $"{nameof(m_prefabToUse)}: {m_prefabToUse}, " +
                   ")";
        }

        public enum TargeterTemplateType
        {
            Unknown,
            DynamicCone,
            Laser
        }
    }
}
