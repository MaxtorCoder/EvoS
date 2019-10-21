using System;
using System.Collections.Generic;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Network.NetworkBehaviours;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("ActorMovement")]
    public class ActorMovement : MonoBehaviour
    {
        private static int s_maxSquaresCanMoveToCacheCount = 4;
        public float m_brushTransitionAnimationSpeed;

        public float m_brushTransitionAnimationSpeedEaseTime = 0.4f;

//        private Dictionary<BoardSquare, BoardSquarePathInfo> m_tempClosedSquares = new Dictionary<BoardSquare, BoardSquarePathInfo>();
        internal ActorData m_actor;

        private float m_moveTimeoutTime;

//        private List<ActorMovement.SquaresCanMoveToCacheEntry> m_squaresCanMoveToCache;
        private HashSet<BoardSquare> m_squaresCanMoveTo;
        private HashSet<BoardSquare> m_squareCanMoveToWithQueuedAbility;
        private BoardSquarePathInfo m_gameplayPath;
        private BoardSquarePathInfo m_aestheticPath;

        public ActorMovement()
        {
        }

        public ActorMovement(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override void Awake()
        {
            m_actor = GetComponent<ActorData>();
//            this.m_squaresCanMoveToCache = new List<ActorMovement.SquaresCanMoveToCacheEntry>();
            m_squaresCanMoveTo = new HashSet<BoardSquare>();
            m_squareCanMoveToWithQueuedAbility = new HashSet<BoardSquare>();
        }

        public BoardSquare GetTravelBoardSquare()
        {
            BoardSquare boardSquare = null;
            if (m_gameplayPath != null)
                boardSquare = m_gameplayPath.square;
            return boardSquare ?? m_actor.method_74();
        }

        public void UpdateSquaresCanMoveTo()
        {
//            if (NetworkServer.active)
//                ;
//            float horizontalMovement = this.m_actor.RemainingHorizontalMovement;
//            float innerMoveDist = this.m_actor.RemainingMovementWithQueuedAbility;
//            BoardSquare squareToStartFrom = this.m_actor.MoveFromBoardSquare;
//            if ((!(Options_UI.Get() != null) || Options_UI.Get().GetShiftClickForMovementWaypoints() == InputManager.Get().IsKeyBindingHeld(KeyPreference.MovementWaypointModifier) ? (!FirstTurnMovement.CanWaypoint() ? 1 : 0) : 1) != 0 && this.m_actor == GameFlowData.Get().activeOwnedActorData)
//            {
//                horizontalMovement = this.CalculateMaxHorizontalMovement(false, false);
//                innerMoveDist = this.CalculateMaxHorizontalMovement(true, false);
//                squareToStartFrom = this.m_actor.InitialMoveStartSquare;
//            }
//            this.GetSquaresCanMoveTo_InnerOuter(squareToStartFrom, horizontalMovement, innerMoveDist, out this.m_squaresCanMoveTo, out this.m_squareCanMoveToWithQueuedAbility);
//            if (Board.smethod_0() != null)
//                Board.smethod_0().MarkForUpdateValidSquares(true);
//            if (!(this.m_actor == GameFlowData.Get().activeOwnedActorData))
//                return;
//            LineData component = this.m_actor.GetComponent<LineData>();
//            if (!(component != null))
//                return;
//            component.OnCanMoveToSquaresUpdated();
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_brushTransitionAnimationSpeed = stream.ReadSingle(); // float32
            m_brushTransitionAnimationSpeedEaseTime = stream.ReadSingle(); // float32
        }

        public override string ToString()
        {
            return $"{nameof(ActorMovement)}(" +
                   $"{nameof(m_brushTransitionAnimationSpeed)}: {m_brushTransitionAnimationSpeed}, " +
                   $"{nameof(m_brushTransitionAnimationSpeedEaseTime)}: {m_brushTransitionAnimationSpeedEaseTime}, " +
                   ")";
        }
    }
}
