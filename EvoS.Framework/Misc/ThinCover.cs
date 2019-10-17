using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Game;
using EvoS.Framework.Logging;
using EvoS.Framework.Network.NetworkBehaviours;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("ThinCover")]
    public class ThinCover : MonoBehaviour, IGameEventListener
    {
        public CoverType m_coverType;

        public ThinCover()
        {
        }

        public ThinCover(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override void Awake()
        {
            GameEventManager.AddListener(this, GameEventManager.EventType.GameFlowDataStarted);
        }

        public override void OnDestroy()
        {
            GameEventManager.RemoveListener(this, GameEventManager.EventType.GameFlowDataStarted);
        }

        void IGameEventListener.OnGameEvent(
            GameEventManager.EventType eventType,
            GameEventManager.GameEventArgs args)
        {
            if (eventType != GameEventManager.EventType.GameFlowDataStarted)
                return;
            if (transform == null)
                Log.Print(LogType.Error,
                    "ThinCover receiving GameFlowDataStarted game event, but its transform is null.");
            else if (GameFlowData == null)
                Log.Print(LogType.Error,
                    "ThinCover receiving GameFlowDataStarted game event, but GameFlowData is null.");
            else if (GameFlowData.GetThinCoverRoot() == null)
            {
                Log.Print(LogType.Error,
                    "ThinCover receiving GameFlowDataStarted game event, but GameFlowData's ThinCoverRoot is null.");
            }
            else
            {
                try
                {
                    transform.father = GameFlowData.GetThinCoverRoot().transform;
                    UpdateBoardSquare();
                }
                catch (NullReferenceException ex)
                {
                    Log.Print(LogType.Error,
                        "Caught System.NullReferenceException for ThinCover receiving GameFlowDataStarted game event.  Highly unexpected!");
                }
            }
        }

        private void UpdateBoardSquare()
        {
            var position = transform.position;
            var squareSize = Board.squareSize;
            var f1 = position.X / squareSize;
            var f2 = position.Z / squareSize;
            var x = Mathf.RoundToInt(f1);
            var y = Mathf.RoundToInt(f2);
            var f3 = f1 - x;
            var f4 = f2 - y;
            Board board = Board;
            if (Mathf.Abs(f3) > (double) Mathf.Abs(f4))
            {
                if (f3 > 0.0)
                {
                    board.SetThinCover(x, y, ActorCover.CoverDirections.X_POS, m_coverType);
                    if (y + 1 >= board.method_4())
                        return;
                    board.SetThinCover(x + 1, y, ActorCover.CoverDirections.X_NEG, m_coverType);
                }
                else
                {
                    board.SetThinCover(x, y, ActorCover.CoverDirections.X_NEG, m_coverType);
                    if (x - 1 < 0)
                        return;
                    board.SetThinCover(x - 1, y, ActorCover.CoverDirections.X_POS, m_coverType);
                }
            }
            else if (f4 > 0.0)
            {
                board.SetThinCover(x, y, ActorCover.CoverDirections.Y_POS, m_coverType);
                if (y + 1 >= board.method_4())
                    return;
                board.SetThinCover(x, y + 1, ActorCover.CoverDirections.Y_NEG, m_coverType);
            }
            else
            {
                board.SetThinCover(x, y, ActorCover.CoverDirections.Y_NEG, m_coverType);
                if (y - 1 < 0)
                    return;
                board.SetThinCover(x, y - 1, ActorCover.CoverDirections.Y_POS, m_coverType);
            }
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_coverType = (CoverType) stream.ReadInt32();
        }

        public override string ToString()
        {
            return $"{nameof(ThinCover)}(" +
                   $"{nameof(m_coverType)}: {m_coverType}, " +
                   ")";
        }

        public enum CoverType
        {
            None,
            Half,
            Full
        }
    }
}
