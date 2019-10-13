using System;
using EvoS.Framework.Misc;
using EvoS.Framework.Network.NetworkBehaviours;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    public struct VisionProviderInfo
    {
        public int m_actorIndex;
        public int m_satelliteIndex;
        public int m_boardX;
        public int m_boardY;
        public float m_radius;
        public bool m_radiusAsStraightLineDist;
        public BoardSquare.VisibilityFlags m_flag;
        public BrushRevealType m_brushRevealType;
        public bool m_ignoreLos;
        public bool m_canFunctionInGlobalBlind;

        public VisionProviderInfo(
            GridPos gridPos,
            float r,
            bool radiusAsStraightLineDist,
            BrushRevealType brushRevealType,
            bool ignoreLos,
            bool canFunctionInGlobalBlind,
            BoardSquare.VisibilityFlags f = BoardSquare.VisibilityFlags.Team)
        {
            m_actorIndex = ActorData.s_invalidActorIndex;
            m_satelliteIndex = -1;
            m_boardX = gridPos.X;
            m_boardY = gridPos.Y;
            m_radius = r;
            m_radiusAsStraightLineDist = radiusAsStraightLineDist;
            m_flag = f;
            m_brushRevealType = brushRevealType;
            m_ignoreLos = ignoreLos;
            m_canFunctionInGlobalBlind = canFunctionInGlobalBlind;
        }

        public VisionProviderInfo(
            int actorIdx,
            float r,
            bool radiusAsStraightLineDist,
            BrushRevealType brushRevealType,
            bool ignoreLos,
            bool canFunctionInGlobalBlind,
            BoardSquare.VisibilityFlags f = BoardSquare.VisibilityFlags.Team)
        {
            m_actorIndex = actorIdx;
            m_satelliteIndex = -1;
            m_boardX = -1;
            m_boardY = -1;
            m_radius = r;
            m_radiusAsStraightLineDist = radiusAsStraightLineDist;
            m_flag = f;
            m_brushRevealType = brushRevealType;
            m_ignoreLos = ignoreLos;
            m_canFunctionInGlobalBlind = canFunctionInGlobalBlind;
        }

        public VisionProviderInfo(
            int actorIdx,
            int satelliteIdx,
            float r,
            bool radiusAsStraightLineDist,
            BrushRevealType brushRevealType,
            bool ignoreLos,
            bool canFunctionInGlobalBlind,
            BoardSquare.VisibilityFlags f = BoardSquare.VisibilityFlags.Team)
        {
            m_actorIndex = actorIdx;
            m_satelliteIndex = satelliteIdx;
            m_boardX = -1;
            m_boardY = -1;
            m_radius = r;
            m_radiusAsStraightLineDist = radiusAsStraightLineDist;
            m_flag = f;
            m_brushRevealType = brushRevealType;
            m_ignoreLos = ignoreLos;
            m_canFunctionInGlobalBlind = canFunctionInGlobalBlind;
        }

        public bool IsEqual(VisionProviderInfo other)
        {
            return IsEqual(new GridPos(other.m_boardX, other.m_boardY, 0), other.m_radius,
                other.m_radiusAsStraightLineDist, other.m_brushRevealType, other.m_ignoreLos, other.m_flag,
                other.m_canFunctionInGlobalBlind);
        }

        public bool IsEqual(
            GridPos gridPos,
            float r,
            bool useSraightLineDist,
            BrushRevealType brushRevealType,
            bool ignoreLos,
            BoardSquare.VisibilityFlags f,
            bool canFunctionInGlobalBlind)
        {
            if (m_actorIndex == ActorData.s_invalidActorIndex && m_boardX == gridPos.X &&
                m_boardY == gridPos.Y)
                return HasSameProperties(r, useSraightLineDist, f, brushRevealType, ignoreLos,
                    canFunctionInGlobalBlind);
            return false;
        }

        public bool IsEqual(
            int actorIdx,
            float r,
            bool useSraightLineDist,
            BrushRevealType brushRevealType,
            bool ignoreLos,
            BoardSquare.VisibilityFlags f,
            bool canFunctionInGlobalBlind)
        {
            if (m_actorIndex == actorIdx && m_satelliteIndex == -1)
                return HasSameProperties(r, useSraightLineDist, f, brushRevealType, ignoreLos,
                    canFunctionInGlobalBlind);
            return false;
        }

        public bool IsEqual(
            int actorIdx,
            int satelliteIdx,
            float r,
            bool useSraightLineDist,
            BrushRevealType brushRevealType,
            bool ignoreLos,
            BoardSquare.VisibilityFlags f,
            bool canFunctionInGlobalBlind)
        {
            if (m_actorIndex == actorIdx && m_satelliteIndex == satelliteIdx)
                return HasSameProperties(r, useSraightLineDist, f, brushRevealType, ignoreLos,
                    canFunctionInGlobalBlind);
            return false;
        }

        private bool HasSameProperties(
            float r,
            bool useSraightLineDist,
            BoardSquare.VisibilityFlags f,
            BrushRevealType brushRevealType,
            bool ignoreLos,
            bool canFunctionInGlobalBlind)
        {
            if (m_flag == f && m_brushRevealType == brushRevealType &&
                (m_ignoreLos == ignoreLos && m_canFunctionInGlobalBlind == canFunctionInGlobalBlind) &&
                m_radiusAsStraightLineDist == useSraightLineDist)
                return Mathf.Abs(m_radius - r) < 0.0500000007450581;
            return false;
        }

        public enum BrushRevealType
        {
            Never,
            BaseOnCenterPosition,
            Always
        }
    }
}
