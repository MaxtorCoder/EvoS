using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.NetworkBehaviours
{
    [Serializable]
    [SerializedMonoBehaviour("TeamSelectData")]
    public class TeamSelectData : NetworkBehaviour
    {
        public TeamSelectData()
        {
        }

        public TeamSelectData(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override bool OnSerialize(NetworkWriter writer, bool forceAll)
        {
            return false;
        }

        public override void OnDeserialize(NetworkReader reader, bool initialState)
        {
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
        }

        public override string ToString()
        {
            return $"{nameof(TeamSelectData)}(" +
                   ")";
        }

        private class TeamStatusEntry
        {
            public string m_text;
        }
    }
}
