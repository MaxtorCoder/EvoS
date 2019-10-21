using System;
using System.Collections.Generic;
using System.Net.Mime;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Logging;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("BoardQuad")]
    public class BoardQuad : ISerializedItem
    {
        public string m_name;
        public Transform m_corner1;
        public Transform m_corner2;

        public BoardQuad()
        {
        }

        public BoardQuad(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public List<BoardSquare> GetSquares(Component context)
        {
            List<BoardSquare> boardSquareList;
            if (m_corner1 != null && m_corner2 != null)
            {
                boardSquareList = context.Board.method_21(
                    m_corner1.GetComponent<BoardSquare>() != null
                        ? m_corner1.GetComponent<BoardSquare>()
                        : context.Board.method_9(m_corner1),
                    m_corner2.GetComponent<BoardSquare>() != null
                        ? m_corner2.GetComponent<BoardSquare>()
                        : context.Board.method_9(m_corner2));
            }
            else
            {
                boardSquareList = new List<BoardSquare>();
                Log.Print(LogType.Debug, $"BoardRegion {m_name} has a BoardQuad with null corners.");
            }

            return boardSquareList;
        }

        public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_name = stream.ReadString32();
            m_corner1 = (Transform) new SerializedComponent(assetFile, stream).LoadValue();
            m_corner2 = (Transform) new SerializedComponent(assetFile, stream).LoadValue();
        }

        public override string ToString()
        {
            return $"{nameof(BoardQuad)}>(" +
                   $"{nameof(m_name)}: {m_name}, " +
                   $"{nameof(m_corner1)}: {m_corner1}, " +
                   $"{nameof(m_corner2)}: {m_corner2}, " +
                   ")";
        }
    }
}
