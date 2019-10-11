using System;
using System.Numerics;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("BoardSquare")]
    public class BoardSquare : MonoBehaviour
    {
        private GridPosProp _gridPosProp;
        private GridPos _pos = GridPos.Invalid;
        public SerializedComponent m_LOSHighlightObj;
        public SerializedArray<Vector3> m_vertices;

        public int X => _pos.X;
        public int Y => _pos.Y;
        public int Height => _pos.Height;

        public BoardSquare()
        {
        }

        public BoardSquare(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public void ReevaluateSquare()
        {
            _pos.X = _gridPosProp.m_x;
            _pos.Y = _gridPosProp.m_y;
            _pos.Height = _gridPosProp.m_height;
        }

        public override void Awake()
        {
            // TODO
//            for (int index = 0; index < this.m_thinCoverTypes.Length; ++index)
//                this.m_thinCoverTypes[index] = ThinCover.CoverType.None;
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            _gridPosProp = new GridPosProp(assetFile, stream);
            m_LOSHighlightObj = new SerializedComponent(assetFile, stream);
            m_vertices = new SerializedArray<Vector3>(assetFile, stream);
        }

        public override string ToString()
        {
            return $"{nameof(BoardSquare)}>(" +
                   $"{nameof(_gridPosProp)}: {_gridPosProp}, " +
                   $"{nameof(m_LOSHighlightObj)}: {m_LOSHighlightObj}, " +
                   $"{nameof(m_vertices)}: [{string.Join(", ", m_vertices)}], " +
                   ")";
        }
    }
}
