using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Game;
using EvoS.Framework.Logging;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.NetworkBehaviours
{
    [Serializable]
    [SerializedMonoBehaviour("ActorAdditionalVisionProviders")]
    public class ActorAdditionalVisionProviders : NetworkBehaviour
    {
        private static int kListm_visionProviders = -547880216;
        private SyncListVisionProviderInfo m_visionProviders = new SyncListVisionProviderInfo();
        private ActorData m_actorData;

        public SyncListVisionProviderInfo VisionProviders => m_visionProviders;

        static ActorAdditionalVisionProviders()
        {
            RegisterSyncListDelegate(typeof(ActorAdditionalVisionProviders), kListm_visionProviders,
                InvokeSyncListm_visionProviders);
        }

        public ActorAdditionalVisionProviders()
        {
        }

        public ActorAdditionalVisionProviders(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        protected static void InvokeSyncListm_visionProviders(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!EvoSGameConfig.NetworkIsClient)
                Log.Print(LogType.Error, "SyncList m_visionProviders called on server.");
            else
                ((ActorAdditionalVisionProviders) obj).m_visionProviders.HandleMsg(reader);
        }

        public override void Awake()
        {
            m_visionProviders.InitializeBehaviour(this, kListm_visionProviders);
        }

        public override bool OnSerialize(NetworkWriter writer, bool forceAll)
        {
            if (forceAll)
            {
                GeneratedNetworkCode._WriteStructSyncListVisionProviderInfo_None(writer, m_visionProviders);
                return true;
            }

            bool flag = false;
            if (((int) syncVarDirtyBits & 1) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                GeneratedNetworkCode._WriteStructSyncListVisionProviderInfo_None(writer, m_visionProviders);
            }

            if (!flag)
                writer.WritePackedUInt32(syncVarDirtyBits);
            return flag;
        }

        public override void OnDeserialize(NetworkReader reader, bool initialState)
        {
            if (initialState)
            {
                GeneratedNetworkCode._ReadStructSyncListVisionProviderInfo_None(reader, m_visionProviders);
            }
            else
            {
                if (((int) reader.ReadPackedUInt32() & 1) == 0)
                    return;
                GeneratedNetworkCode._ReadStructSyncListVisionProviderInfo_None(reader, m_visionProviders);
            }
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
        }

        public override string ToString()
        {
            return $"{nameof(ActorAdditionalVisionProviders)}>(" +
                   ")";
        }
    }
}
