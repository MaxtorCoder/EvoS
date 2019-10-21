using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("MovementPathStart")]
    public class MovementPathStart : MonoBehaviour
    {
        public SerializedComponent m_InsideMesh;
        public SerializedComponent m_MiddleMesh;
        public SerializedComponent m_OutsideMesh;
        public SerializedComponent m_ArrowMesh;
        public SerializedComponent m_CloseArrowMesh;
        public SerializedComponent m_FarArrowMesh;
        public SerializedComponent m_insideTexture;
        public SerializedComponent m_middleTexture;
        public SerializedComponent m_outsideTexture;
        public SerializedComponent m_arrowTexture;
        public SerializedComponent m_closeArrowTexture;
        public SerializedComponent m_farArrowTexture;
        public SerializedArray<SerializedComponent> m_chasingDiamonds;
        public SerializedComponent m_chasingInnerRing;
        public SerializedComponent m_chasingOuterRing;
        public SerializedComponent m_chasingArrow;
        public SerializedComponent m_chasingDiamondTexture;
        public SerializedComponent m_chasingInnerRingTexture;
        public SerializedComponent m_chasingOuterRingTexture;
        public SerializedComponent m_chasingArrowTexture;
        public SerializedComponent m_KnockbackMesh;
        public SerializedComponent m_animationController;
        public SerializedComponent m_movementContainer;
        public SerializedComponent m_chasingContainer;
        public SerializedComponent m_knockbackContainer;
        public SerializedComponent m_chasingDiamondsContainer;
        public SerializedComponent endPiece;
        public SerializedComponent linePiece;

        public MovementPathStart()
        {
        }

        public MovementPathStart(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_InsideMesh = new SerializedComponent(assetFile, stream);
            m_MiddleMesh = new SerializedComponent(assetFile, stream);
            m_OutsideMesh = new SerializedComponent(assetFile, stream);
            m_ArrowMesh = new SerializedComponent(assetFile, stream);
            m_CloseArrowMesh = new SerializedComponent(assetFile, stream);
            m_FarArrowMesh = new SerializedComponent(assetFile, stream);
            m_insideTexture = new SerializedComponent(assetFile, stream);
            m_middleTexture = new SerializedComponent(assetFile, stream);
            m_outsideTexture = new SerializedComponent(assetFile, stream);
            m_arrowTexture = new SerializedComponent(assetFile, stream);
            m_closeArrowTexture = new SerializedComponent(assetFile, stream);
            m_farArrowTexture = new SerializedComponent(assetFile, stream);
            m_chasingDiamonds = new SerializedArray<SerializedComponent>(assetFile, stream);
            m_chasingInnerRing = new SerializedComponent(assetFile, stream);
            m_chasingOuterRing = new SerializedComponent(assetFile, stream);
            m_chasingArrow = new SerializedComponent(assetFile, stream);
            m_chasingDiamondTexture = new SerializedComponent(assetFile, stream);
            m_chasingInnerRingTexture = new SerializedComponent(assetFile, stream);
            m_chasingOuterRingTexture = new SerializedComponent(assetFile, stream);
            m_chasingArrowTexture = new SerializedComponent(assetFile, stream);
            m_KnockbackMesh = new SerializedComponent(assetFile, stream);
            m_animationController = new SerializedComponent(assetFile, stream);
            m_movementContainer = new SerializedComponent(assetFile, stream);
            m_chasingContainer = new SerializedComponent(assetFile, stream);
            m_knockbackContainer = new SerializedComponent(assetFile, stream);
            m_chasingDiamondsContainer = new SerializedComponent(assetFile, stream);
            endPiece = new SerializedComponent(assetFile, stream);
            linePiece = new SerializedComponent(assetFile, stream);
        }

        public override string ToString()
        {
            return $"{nameof(MovementPathStart)}>(" +
                   $"{nameof(m_InsideMesh)}: {m_InsideMesh}, " +
                   $"{nameof(m_MiddleMesh)}: {m_MiddleMesh}, " +
                   $"{nameof(m_OutsideMesh)}: {m_OutsideMesh}, " +
                   $"{nameof(m_ArrowMesh)}: {m_ArrowMesh}, " +
                   $"{nameof(m_CloseArrowMesh)}: {m_CloseArrowMesh}, " +
                   $"{nameof(m_FarArrowMesh)}: {m_FarArrowMesh}, " +
                   $"{nameof(m_insideTexture)}: {m_insideTexture}, " +
                   $"{nameof(m_middleTexture)}: {m_middleTexture}, " +
                   $"{nameof(m_outsideTexture)}: {m_outsideTexture}, " +
                   $"{nameof(m_arrowTexture)}: {m_arrowTexture}, " +
                   $"{nameof(m_closeArrowTexture)}: {m_closeArrowTexture}, " +
                   $"{nameof(m_farArrowTexture)}: {m_farArrowTexture}, " +
                   $"{nameof(m_chasingDiamonds)}: {m_chasingDiamonds}, " +
                   $"{nameof(m_chasingInnerRing)}: {m_chasingInnerRing}, " +
                   $"{nameof(m_chasingOuterRing)}: {m_chasingOuterRing}, " +
                   $"{nameof(m_chasingArrow)}: {m_chasingArrow}, " +
                   $"{nameof(m_chasingDiamondTexture)}: {m_chasingDiamondTexture}, " +
                   $"{nameof(m_chasingInnerRingTexture)}: {m_chasingInnerRingTexture}, " +
                   $"{nameof(m_chasingOuterRingTexture)}: {m_chasingOuterRingTexture}, " +
                   $"{nameof(m_chasingArrowTexture)}: {m_chasingArrowTexture}, " +
                   $"{nameof(m_KnockbackMesh)}: {m_KnockbackMesh}, " +
                   $"{nameof(m_animationController)}: {m_animationController}, " +
                   $"{nameof(m_movementContainer)}: {m_movementContainer}, " +
                   $"{nameof(m_chasingContainer)}: {m_chasingContainer}, " +
                   $"{nameof(m_knockbackContainer)}: {m_knockbackContainer}, " +
                   $"{nameof(m_chasingDiamondsContainer)}: {m_chasingDiamondsContainer}, " +
                   $"{nameof(endPiece)}: {endPiece}, " +
                   $"{nameof(linePiece)}: {linePiece}, " +
                   ")";
        }
    }
}
