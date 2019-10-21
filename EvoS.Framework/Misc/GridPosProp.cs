using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("GridPosProp")]
    public class GridPosProp : ISerializedItem
    {
        public int m_x;
        public int m_y;
        public int m_height;

        public GridPosProp()
        {
        }

        public GridPosProp(int x, int y, int height)
        {
            m_x = x;
            m_y = y;
            m_height = height;
        }

        public GridPosProp(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_x = stream.ReadInt32();
            m_y = stream.ReadInt32();
            m_height = stream.ReadInt32();
        }

        public override string ToString()
        {
            return $"{nameof(GridPosProp)}>(" +
                   $"{nameof(m_x)}: {m_x}, " +
                   $"{nameof(m_y)}: {m_y}, " +
                   $"{nameof(m_height)}: {m_height}, " +
                   ")";
        }
    }
}
