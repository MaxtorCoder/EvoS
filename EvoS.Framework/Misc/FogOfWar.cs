using System;
using System.Collections.Generic;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Network.NetworkBehaviours;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    [Serializable]
    [SerializedMonoBehaviour("FogOfWar")]
    public class FogOfWar : MonoBehaviour
    {
        private bool m_updateVisibility;
        private bool m_updatedVisibilityThisFrame;
        private bool m_visibilityPersonalOnly;
        private PlayerData m_ownerPlayer;
        private ActorData m_owner;
        private Dictionary<BoardSquare, VisibleSquareEntry> m_visibleSquares;
        private float m_lastRecalcTime;
        
        public FogOfWar()
        {
        }

        public FogOfWar(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public void MarkForRecalculateVisibility()
        {
            this.m_updateVisibility = true;
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
        }

        public override string ToString()
        {
            return $"{nameof(FogOfWar)}(" +
                   ")";
        }

        private struct VisibleSquareEntry
        {
            public int m_visibleFlags;
        }
    }
}
