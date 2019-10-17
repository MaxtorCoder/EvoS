using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("PatrolPath")]
    public class PatrolPath : MonoBehaviour
    {
        public SerializedVector<WayPoint> mWayPoints;
        public SerializedComponent InitalWaypoint;
        public PatrolStyle m_PatrolStyle;
        public Direction m_Direction;
        public string m_Tag;

        public PatrolPath()
        {
        }

        public PatrolPath(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            mWayPoints = new SerializedVector<WayPoint>(assetFile, stream);
            InitalWaypoint = new SerializedComponent(assetFile, stream);
            m_PatrolStyle = (PatrolStyle) stream.ReadInt32();
            m_Direction = (Direction) stream.ReadInt32();
            m_Tag = stream.ReadString32();
        }

        public override string ToString()
        {
            return $"{nameof(PatrolPath)}(" +
                   $"{nameof(mWayPoints)}: {mWayPoints}, " +
                   $"{nameof(InitalWaypoint)}: {InitalWaypoint}, " +
                   $"{nameof(m_PatrolStyle)}: {m_PatrolStyle}, " +
                   $"{nameof(m_Direction)}: {m_Direction}, " +
                   $"{nameof(m_Tag)}: {m_Tag}, " +
                   ")";
        }

        [Serializable]
        public enum PatrolStyle
        {
            Loop,
            OutAndBack,
            Random
        }

        [Serializable]
        public enum Direction
        {
            Backward = -1,
            Forward = 1
        }

        public enum IncrementWaypointResult
        {
            None,
            Incremented,
            CycleCompleted
        }
    }
}
