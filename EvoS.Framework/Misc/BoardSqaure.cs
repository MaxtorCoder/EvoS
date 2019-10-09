using System;
using System.Numerics;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("BoardSquare")]
    public class BoardSquare : ISerializedItem
    {
        public GridPosProp m_gridPosProp;
        public SerializedComponent m_LOSHighlightObj;
        public SerializedArray<Vector3> m_vertices;

        public BoardSquare()
        {
            
        }
        
        public BoardSquare(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_gridPosProp = new GridPosProp(assetFile, stream);
            m_LOSHighlightObj = new SerializedComponent(assetFile, stream);
            m_vertices = new SerializedArray<Vector3>(assetFile, stream);
        }

        public override string ToString()
        {
            return $"{nameof(BoardSquare)}>(" +
                   $"{nameof(m_gridPosProp)}: {m_gridPosProp}, " +
                   $"{nameof(m_LOSHighlightObj)}: {m_LOSHighlightObj}, " +
                   $"{nameof(m_vertices)}: [{string.Join(", ", m_vertices)}], " +
                   ")";
        }
    }
}
