using System;
using System.Collections;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Network.Unity;

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
        public int MaxX { get; private set; }
        public int MaxY { get; private set; }
        public int MaxHeight { get; private set; }
        public int LastValidGuidedHeight { get; private set; }
        public int LowestPositiveHeight { get; private set; }
        private BoardSquare[,] _boardSquares;

        public Board()
        {
        }

        public Board(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }
        
//        public void ReevaluateBoard()
//        {
//            MaxX = 0;
//            MaxY = 0;
//            MaxHeight = 0;
//            LastValidGuidedHeight = 99999;
//            LowestPositiveHeight = 99999;
//            IEnumerator enumerator1 = this.transform.GetEnumerator();
//            try
//            {
//                while (enumerator1.MoveNext())
//                {
//                    BoardSquare component = ((Component) enumerator1.Current).GetComponent<BoardSquare>();
//                    component.ReevaluateSquare();
//                    if (component.Height > 0 && component.Height < LowestPositiveHeight)
//                        LowestPositiveHeight = component.Height;
//                    if (component.Height > MaxHeight)
//                        MaxHeight = component.Height;
//                    if (component.X + 1 > MaxX)
//                        MaxX = component.X + 1;
//                    if (component.Y + 1 > MaxY)
//                        MaxY = component.Y + 1;
//                }
//            }
//            finally
//            {
//                if (enumerator1 is IDisposable disposable)
//                    disposable.Dispose();
//            }
//            _boardSquares = new BoardSquare[MaxX, MaxY];
//            IEnumerator enumerator2 = this.transform.GetEnumerator();
//            try
//            {
//                while (enumerator2.MoveNext())
//                {
//                    BoardSquare component = ((Component) enumerator2.Current)?.GetComponent<BoardSquare>();
//                    if (component != null)
//                        _boardSquares[component.X, component.Y] = component;
//                }
//            }
//            finally
//            {
//                if (enumerator2 is IDisposable disposable)
//                    disposable.Dispose();
//            }
//        }

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

        public BoardSquare GetBoardSquare(int x, int y)
        {
                if (x >= 0 && x < MaxX && (y >= 0 && y < MaxY))
                  return _boardSquares[x, y];
                return null;
        }
    }
}
