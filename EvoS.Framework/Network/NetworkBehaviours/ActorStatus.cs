using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Game;
using EvoS.Framework.Logging;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.NetworkBehaviours
{
    [Serializable]
    [SerializedMonoBehaviour("ActorStatus")]
    public class ActorStatus : NetworkBehaviour
    {
        private static int kListm_statusCounts = -7231791;
        private static int kListm_statusDurations = 625641650;
        private SyncListUInt _statusCounts = new SyncListUInt();
        private SyncListUInt _statusDurations = new SyncListUInt();
        private int[] _statusCountsPrevious;
        private int[] _clientStatusCountAdjustments;
        private ActorData _actorData;

        public SyncListUInt StatusCounts => _statusCounts;
        public SyncListUInt StatusDurations => _statusDurations;
        public int[] StatusCountsPrevious => _statusCountsPrevious;
        public int[] ClientStatusCountAdjustments => _clientStatusCountAdjustments;

        static ActorStatus()
        {
            RegisterSyncListDelegate(typeof(ActorStatus), kListm_statusCounts, InvokeSyncListm_statusCounts);
            RegisterSyncListDelegate(typeof(ActorStatus), kListm_statusDurations, InvokeSyncListm_statusDurations);
        }

        public ActorStatus()
        {
        }

        public ActorStatus(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override void Awake()
        {
            _statusCountsPrevious = new int[58];
            _clientStatusCountAdjustments = new int[58];
            _actorData = GetComponent<ActorData>();
            _statusCounts.InitializeBehaviour(this, kListm_statusCounts);
            _statusDurations.InitializeBehaviour(this, kListm_statusDurations);
        }

        protected static void InvokeSyncListm_statusCounts(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!EvoSGameConfig.NetworkIsClient)
                Log.Print(LogType.Error, "SyncList m_statusCounts called on server.");
            else
                ((ActorStatus) obj)._statusCounts.HandleMsg(reader);
        }

        protected static void InvokeSyncListm_statusDurations(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!EvoSGameConfig.NetworkIsClient)
                Log.Print(LogType.Error, "SyncList m_statusDurations called on server.");
            else
                ((ActorStatus) obj)._statusDurations.HandleMsg(reader);
        }

        public override bool OnSerialize(NetworkWriter writer, bool forceAll)
        {
            if (forceAll)
            {
                SyncListUInt.WriteInstance(writer, _statusCounts);
                SyncListUInt.WriteInstance(writer, _statusDurations);
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

                SyncListUInt.WriteInstance(writer, _statusCounts);
            }

            if (((int) syncVarDirtyBits & 2) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                SyncListUInt.WriteInstance(writer, _statusDurations);
            }

            if (!flag)
                writer.WritePackedUInt32(syncVarDirtyBits);
            return flag;
        }

        public override void OnDeserialize(NetworkReader reader, bool initialState)
        {
            if (initialState)
            {
                SyncListUInt.ReadReference(reader, _statusCounts);
                SyncListUInt.ReadReference(reader, _statusDurations);
            }
            else
            {
                int num = (int) reader.ReadPackedUInt32();
                if ((num & 1) != 0)
                    SyncListUInt.ReadReference(reader, _statusCounts);
                if ((num & 2) == 0)
                    return;
                SyncListUInt.ReadReference(reader, _statusDurations);
            }
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
        }

        public override string ToString()
        {
            return $"{nameof(ActorStatus)}>(" +
                   ")";
        }
    }
}
