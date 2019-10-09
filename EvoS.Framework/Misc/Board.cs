using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("Board")]
    public class Board : ISerializedItem
    {
        public SerializedComponent m_LOSHighlightsParent;
        public bool m_showLOS;
        public float m_squareSize;
        public int m_baselineHeight;
        public SerializedComponent m_losLookup;

        public Board()
        {
        }

        public Board(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_LOSHighlightsParent = new SerializedComponent(assetFile, stream);
            m_showLOS = stream.ReadBoolean();
            stream.AlignTo();
            m_squareSize = stream.ReadSingle();
            m_baselineHeight = stream.ReadInt32();
            m_losLookup = new SerializedComponent(assetFile, stream);
        }

        public override string ToString()
        {
            return $"{nameof(Board)}>(" +
                   $"{nameof(m_LOSHighlightsParent)}: {m_LOSHighlightsParent}, " +
                   $"{nameof(m_showLOS)}: {m_showLOS}, " +
                   $"{nameof(m_squareSize)}: {m_squareSize}, " +
                   $"{nameof(m_baselineHeight)}: {m_baselineHeight}, " +
                   $"{nameof(m_losLookup)}: {m_losLookup}, " +
                   ")";
        }
    }
}
