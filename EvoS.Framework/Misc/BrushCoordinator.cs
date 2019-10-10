using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("BrushCoordinator")]
    public class BrushCoordinator : ISerializedItem
    {
        public SerializedArray<BrushRegion> m_regions;

        public BrushCoordinator()
        {
            
        }
        
        public BrushCoordinator(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_regions = new SerializedArray<BrushRegion>(assetFile, stream); // class BrushRegion[]
        }

        public override string ToString()
        {
            return $"{nameof(BrushCoordinator)}>(" +
                   $"{nameof(m_regions)}: [{string.Join(", ", m_regions)}]" +
                   ")";
        }
    }
}
