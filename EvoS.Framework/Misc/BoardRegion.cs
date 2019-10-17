using System;
using System.Collections.Generic;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Logging;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("BoardRegion")]
    public class BoardRegion : ISerializedItem
    {
        public SerializedArray<BoardQuad> m_quads;
        private List<BoardSquare> m_squaresInRegion;

        public BoardRegion()
        {
        }

        public BoardRegion(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        private void CacheSquaresInRegion(Component context)
        {
            m_squaresInRegion = new List<BoardSquare>();
            if (m_quads == null)
                return;
            foreach (var quad in m_quads)
            {
                if (quad == null)
                {
                    Log.Print(LogType.Error, "Null BoardQuad in BoardRegion; fix region coordinator's data.");
                }
                else
                {
                    foreach (var square in quad.GetSquares(context))
                    {
                        if (!m_squaresInRegion.Contains(square))
                            m_squaresInRegion.Add(square);
                    }
                }
            }
        }

        public virtual void Initialize(Component context)
        {
            CacheSquaresInRegion(context);
        }

        public List<BoardSquare> method_0()
        {
            if (m_squaresInRegion == null)
            {
                Log.Print(LogType.Error, "Did not call CacheSquaresInRegion before calling GetSquaresInRegion.  This will cause slowdowns");
                CacheSquaresInRegion(null); // FIXME
            }
            return m_squaresInRegion;
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
