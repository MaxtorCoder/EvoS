using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("BoardRegion")]
    public class BoardRegion : ISerializedItem
    {
        public SerializedArray<BoardQuad> m_quads;

        public BoardRegion()
        {
        }

        public BoardRegion(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public virtual void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_quads = new SerializedArray<BoardQuad>(assetFile, stream);
        }

        public override string ToString()
        {
            return $"{nameof(BoardRegion)}>(" +
                   $"{nameof(m_quads)}: {m_quads}, " +
                   ")";
        }
    }
}
