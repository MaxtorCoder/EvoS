using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("QueryArea")]
    public class QueryArea : MonoBehaviour
    {
        public int m_boardSquareSizeX;
        public int m_boardSquareSizeY;
        public ColorRGBA m_gizmoColor;
        public string m_name;

        public QueryArea()
        {
        }

        public QueryArea(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_boardSquareSizeX = stream.ReadInt32();
            m_boardSquareSizeY = stream.ReadInt32();
            m_gizmoColor = stream.ReadColorRGBA();
            m_name = stream.ReadString32();
        }

        public override string ToString()
        {
            return $"{nameof(QueryArea)}(" +
                   $"{nameof(m_boardSquareSizeX)}: {m_boardSquareSizeX}, " +
                   $"{nameof(m_boardSquareSizeY)}: {m_boardSquareSizeY}, " +
                   $"{nameof(m_gizmoColor)}: {m_gizmoColor}, " +
                   $"{nameof(m_name)}: {m_name}, " +
                   ")";
        }
    }
}
