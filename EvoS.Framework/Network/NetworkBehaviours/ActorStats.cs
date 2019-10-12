using System;
using System.Collections.Generic;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Constants.Enums;
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

        public ActorStats()
        {
        }

        public ActorStats(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
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
