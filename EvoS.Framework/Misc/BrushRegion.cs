using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("BrushRegion")]
    public class BrushRegion : BoardRegion
    {
        public SerializedComponent m_functioningVFX;
        public SerializedComponent m_disruptedVFX;
        public SerializedVector<SerializedComponent> m_perSquareFunctioningVFX;
        public SerializedVector<SerializedComponent> m_perSquareDisruptedVFX;

        public BrushRegion()
        {
        }

        public BrushRegion(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            base.DeserializeAsset(assetFile, stream);

            m_functioningVFX = new SerializedComponent(assetFile, stream);
            m_disruptedVFX = new SerializedComponent(assetFile, stream);
            m_perSquareFunctioningVFX = new SerializedVector<SerializedComponent>(assetFile, stream);
            m_perSquareDisruptedVFX = new SerializedVector<SerializedComponent>(assetFile, stream);
        }

        public override string ToString()
        {
            return $"{nameof(BrushRegion)}>(" +
                   $"{nameof(m_functioningVFX)}: {m_functioningVFX}, " +
                   $"{nameof(m_disruptedVFX)}: {m_disruptedVFX}, " +
                   $"{nameof(m_perSquareFunctioningVFX)}: {m_perSquareFunctioningVFX}, " +
                   $"{nameof(m_perSquareDisruptedVFX)}: {m_perSquareDisruptedVFX}, " +
                   ")";
        }
    }
}
