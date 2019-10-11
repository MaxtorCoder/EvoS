using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Misc;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.NetworkBehaviours
{
    [Serializable]
    [SerializedMonoBehaviour("BrushCoordinator")]
    public class BrushCoordinator : NetworkBehaviour
    {
        public SerializedArray<BrushRegion> m_regions;

        public BrushCoordinator()
        {
            
        }
        
        public BrushCoordinator(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
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
