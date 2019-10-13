using System;
using System.Collections.Generic;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Game;
using EvoS.Framework.Logging;
using EvoS.Framework.Misc;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.NetworkBehaviours
{
    [Serializable]
    [SerializedMonoBehaviour("ActorStats")]
    public class ActorStats : NetworkBehaviour
    {
        private static int kListm_modifiedStats = -1034899976;
        private SyncListFloat m_modifiedStats = new SyncListFloat();
        private bool m_shouldUpdateFull;
        private Dictionary<StatType, List<StatMod>> m_statMods;
        private float[] m_modifiedStatsPrevious;
        private ActorData m_actorData;

        public SyncListFloat ModifiedStats => m_modifiedStats;
        public bool ShouldUpdateFull => m_shouldUpdateFull;
        public Dictionary<StatType, List<StatMod>> StatMods => m_statMods;
        public float[] ModifiedStatsPrevious => m_modifiedStatsPrevious;

        static ActorStats()
        {
            NetworkBehaviour.RegisterSyncListDelegate(typeof(ActorStats), ActorStats.kListm_modifiedStats,
                new NetworkBehaviour.CmdDelegate(ActorStats.InvokeSyncListm_modifiedStats));
        }

        public ActorStats()
        {
        }

        public ActorStats(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override void Awake()
        {
            m_statMods =
                new Dictionary<StatType, List<StatMod>>(
                    new FuncEqualityComparer<StatType>((a, b) => a == b, a => (int) a));
            for (var index = 0; index < 24; ++index)
            {
                var statModList = new List<StatMod>();
                m_statMods.Add((StatType) index, statModList);
            }

            m_modifiedStats.InitializeBehaviour(this, kListm_modifiedStats);
            m_modifiedStatsPrevious = new float[24];
            m_actorData = GetComponent<ActorData>();
        }

        protected static void InvokeSyncListm_modifiedStats(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!EvoSGameConfig.NetworkIsClient)
                Log.Print(LogType.Error, "SyncList _modifiedStats called on server.");
            else
                ((ActorStats) obj).m_modifiedStats.HandleMsg(reader);
        }

        public override bool OnSerialize(NetworkWriter writer, bool forceAll)
        {
            if (forceAll)
            {
                SyncListFloat.WriteInstance(writer, m_modifiedStats);
                return true;
            }

            var flag = false;
            if (((int) syncVarDirtyBits & 1) != 0)
            {
                writer.WritePackedUInt32(syncVarDirtyBits);
                flag = true;

                SyncListFloat.WriteInstance(writer, m_modifiedStats);
            }

            if (!flag)
                writer.WritePackedUInt32(syncVarDirtyBits);
            return flag;
        }

        public override void OnDeserialize(NetworkReader reader, bool initialState)
        {
            if (initialState)
            {
                SyncListFloat.ReadReference(reader, m_modifiedStats);
            }
            else
            {
                if (((int) reader.ReadPackedUInt32() & 1) == 0)
                    return;
                SyncListFloat.ReadReference(reader, m_modifiedStats);
            }
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
        }

        public override string ToString()
        {
            return $"{nameof(ActorStats)}>(" +
                   ")";
        }
    }
}
