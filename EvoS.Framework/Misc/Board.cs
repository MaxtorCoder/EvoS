using System;
using System.Collections.Generic;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("Board")]
    public class Board : MonoBehaviour
    {
        public SerializedComponent m_LOSHighlightsParent;
        public bool m_showLOS;
        public float m_squareSize = 1f;
        public int m_baselineHeight = -1;
        public SerializedComponent m_losLookup;
        private BoardSquare[,] _boardSquares;

        private int m_lowestPositiveHeight = 9999;
        private int m_lastValidGuidedHeight = 99999;
        public HashSet<BoardSquare> m_highlightedBoardSquares = new HashSet<BoardSquare>();
        private bool m_needToUpdateValidSquares = true;
        private int m_maxX;
        private int m_maxY;
        private int m_maxHeight;

//        private MeshCollider m_cameraGuideMeshCollider;
//        internal BuildNormalPathNodePool m_normalPathBuildScratchPool;
//        internal BuildNormalPathHeap m_normalPathNodeHeap;
        public float squareSize => m_squareSize;

        public int BaselineHeight
        {
            get
            {
                if (m_baselineHeight >= 0)
                    return m_baselineHeight;
                return m_lowestPositiveHeight;
            }
        }

        public float LosCheckHeight => BaselineHeight + BoardSquare.s_LoSHeightOffset;

        public Board()
        {
        }

        public Board(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override void Awake()
        {
            enabled = false;
//            GameEventManager.Get().AddListener((IGameEventListener) this, GameEventManager.EventType.GameFlowDataStarted);
//            GameObject gameObject = GameObject.Find("Camera Guide Mesh");
//            if (gameObject != null)
//                this.m_cameraGuideMeshCollider = gameObject.GetComponent<MeshCollider>();
            ReevaluateBoard();
            m_showLOS = true;
//            Board.s_squareSizeStatic = m_squareSize;
//            Board.BaselineHeightStatic = BaselineHeight;
//            this.m_normalPathBuildScratchPool = new BuildNormalPathNodePool();
//            this.m_normalPathNodeHeap = new BuildNormalPathHeap(60);
        }

        public void ReevaluateBoard()
        {
            m_maxX = 0;
            m_maxY = 0;
            m_maxHeight = 0;
            m_lastValidGuidedHeight = 99999;
            m_lowestPositiveHeight = 99999;
            foreach (var child in transform)
            {
                var component = child.GetComponent<BoardSquare>();
                component.ReevaluateSquare();
                if (component.Height > 0 && component.Height < m_lowestPositiveHeight)
                    m_lowestPositiveHeight = component.Height;
                if (component.Height > m_maxHeight)
                    m_maxHeight = component.Height;
                if (component.X + 1 > m_maxX)
                    m_maxX = component.X + 1;
                if (component.Y + 1 > m_maxY)
                    m_maxY = component.Y + 1;
            }

            _boardSquares = new BoardSquare[m_maxX, m_maxY];
            foreach (var child in transform)
            {
                var component = child.GetComponent<BoardSquare>();
                if (component != null)
                    _boardSquares[component.X, component.Y] = component;
            }
        }

        public void MarkForUpdateValidSquares(bool value = true)
        {
            m_needToUpdateValidSquares = value;
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
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
            if (x >= 0 && x < m_maxX && (y >= 0 && y < m_maxY))
                return _boardSquares[x, y];
            return null;
        }
    }
}
