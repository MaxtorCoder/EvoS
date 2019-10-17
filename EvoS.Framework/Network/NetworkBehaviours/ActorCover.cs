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
    [SerializedMonoBehaviour("ActorCover")]
    public class ActorCover : NetworkBehaviour
    {
        private static int kListm_syncTempCoverProviders = 1438054992;
        private SyncListTempCoverInfo m_syncTempCoverProviders = new SyncListTempCoverInfo();
        private ActorData m_owner;

        public SyncListTempCoverInfo SyncTempCoverProviders => m_syncTempCoverProviders;

        static ActorCover()
        {
            RegisterSyncListDelegate(typeof(ActorCover), kListm_syncTempCoverProviders,
                InvokeSyncListm_syncTempCoverProviders);
        }

        public ActorCover()
        {
        }

        public ActorCover(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override void Awake()
        {
            m_owner = GetComponent<ActorData>();
            m_syncTempCoverProviders.InitializeBehaviour(this, kListm_syncTempCoverProviders);
        }

        protected static void InvokeSyncListm_syncTempCoverProviders(
            NetworkBehaviour obj,
            NetworkReader reader)
        {
            if (!EvoSGameConfig.NetworkIsClient)
                Log.Print(LogType.Error, "SyncList m_syncTempCoverProviders called on server.");
            else
                ((ActorCover) obj).m_syncTempCoverProviders.HandleMsg(reader);
        }

        public override bool OnSerialize(NetworkWriter writer, bool forceAll)
        {
            if (forceAll)
            {
                GeneratedNetworkCode._WriteStructSyncListTempCoverInfo_None(writer, m_syncTempCoverProviders);
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

                GeneratedNetworkCode._WriteStructSyncListTempCoverInfo_None(writer, m_syncTempCoverProviders);
            }

            if (!flag)
                writer.WritePackedUInt32(syncVarDirtyBits);
            return flag;
        }

        public override void OnDeserialize(NetworkReader reader, bool initialState)
        {
            if (initialState)
            {
                GeneratedNetworkCode._ReadStructSyncListTempCoverInfo_None(reader, m_syncTempCoverProviders);
            }
            else
            {
                if (((int) reader.ReadPackedUInt32() & 1) == 0)
                    return;
                GeneratedNetworkCode._ReadStructSyncListTempCoverInfo_None(reader, m_syncTempCoverProviders);
            }
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
        }

        public override string ToString()
        {
            return $"{nameof(ActorCover)}>(" +
                   ")";
        }

        public enum CoverDirections
        {
            INVALID = -1, // 0xFFFFFFFF
            FIRST = 0,
            X_POS = 0,
            X_NEG = 1,
            Y_POS = 2,
            Y_NEG = 3,
            NUM = 4
        }
    }
}
