using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("MovementPathEnd")]
    public class MovementPathEnd : MonoBehaviour
    {
        public SerializedComponent m_TopIndicatorPiece;
        public SerializedComponent m_IndicatorParent;
        public SerializedComponent m_diamondContainer;
        public SerializedComponent m_movementLineParent;
        public SerializedComponent m_chasingParent;
        public SerializedComponent m_animationController;

        public MovementPathEnd()
        {
            
        }
        
        public MovementPathEnd(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_TopIndicatorPiece = new SerializedComponent(assetFile, stream);
            m_IndicatorParent = new SerializedComponent(assetFile, stream);
            m_diamondContainer = new SerializedComponent(assetFile, stream);
            m_movementLineParent = new SerializedComponent(assetFile, stream);
            m_chasingParent = new SerializedComponent(assetFile, stream);
            m_animationController = new SerializedComponent(assetFile, stream);
        }

        public override string ToString()
        {
            return $"{nameof(MovementPathEnd)}>(" +
                   $"{nameof(m_TopIndicatorPiece)}: {m_TopIndicatorPiece}, " +
                   $"{nameof(m_IndicatorParent)}: {m_IndicatorParent}, " +
                   $"{nameof(m_diamondContainer)}: {m_diamondContainer}, " +
                   $"{nameof(m_movementLineParent)}: {m_movementLineParent}, " +
                   $"{nameof(m_chasingParent)}: {m_chasingParent}, " +
                   $"{nameof(m_animationController)}: {m_animationController}, " +
                   ")";
        }
    }
}
