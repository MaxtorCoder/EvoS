using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Game;
using EvoS.Framework.Logging;
using EvoS.Framework.Network.Unity;
using Newtonsoft.Json;

namespace EvoS.Framework.Network.NetworkBehaviours
{
    [Serializable]
    [SerializedMonoBehaviour("ActorCinematicRequests")]
    public class ActorCinematicRequests : NetworkBehaviour
    {
        private static int kCmdCmdSelectAbilityCinematicRequest = 1550121236;

        internal SyncListBool m_abilityRequested = new SyncListBool();

//        [SyncVar]
        private int m_numCinematicRequestsLeft = 2;
        private SyncListInt m_cinematicsPlayedThisMatch = new SyncListInt();
        private ActorData m_actorData;
        private static int kListm_abilityRequested = -88780988;
        private static int kListm_cinematicsPlayedThisMatch = 782922590;

        public SyncListBool AbilityRequested => m_abilityRequested;
        public int NumCinematicRequestsLeft => m_numCinematicRequestsLeft;
        public SyncListInt CinematicsPlayedThisMatch => m_cinematicsPlayedThisMatch;
        [JsonIgnore]
        public ActorData ActorData => m_actorData;

        static ActorCinematicRequests()
        {
            RegisterSyncListDelegate(typeof(ActorCinematicRequests), kListm_abilityRequested,
                InvokeSyncListm_abilityRequested);
            RegisterSyncListDelegate(typeof(ActorCinematicRequests), kListm_cinematicsPlayedThisMatch,
                InvokeSyncListm_cinematicsPlayedThisMatch);
        }

        public override void Awake()
        {
            m_actorData = GetComponent<ActorData>();
            m_abilityRequested.InitializeBehaviour(this, kListm_abilityRequested);
            m_cinematicsPlayedThisMatch.InitializeBehaviour(this, kListm_cinematicsPlayedThisMatch);
        }

        public ActorCinematicRequests()
        {
        }

        public ActorCinematicRequests(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
        }


        protected static void InvokeSyncListm_abilityRequested(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!EvoSGameConfig.NetworkIsClient)
                Log.Print(LogType.Error, "SyncList m_abilityRequested called on server.");
            else
                ((ActorCinematicRequests) obj).m_abilityRequested.HandleMsg(reader);
        }

        protected static void InvokeSyncListm_cinematicsPlayedThisMatch(
            NetworkBehaviour obj,
            NetworkReader reader)
        {
            if (!EvoSGameConfig.NetworkIsClient)
                Log.Print(LogType.Error, "SyncList m_cinematicsPlayedThisMatch called on server.");
            else
                ((ActorCinematicRequests) obj).m_cinematicsPlayedThisMatch.HandleMsg(reader);
        }

        public override bool OnSerialize(NetworkWriter writer, bool forceAll)
        {
            if (forceAll)
            {
                SyncListBool.WriteInstance(writer, m_abilityRequested);
                writer.WritePackedUInt32((uint) m_numCinematicRequestsLeft);
                SyncListInt.WriteInstance(writer, m_cinematicsPlayedThisMatch);
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

                SyncListBool.WriteInstance(writer, m_abilityRequested);
            }

            if (((int) syncVarDirtyBits & 2) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.WritePackedUInt32((uint) m_numCinematicRequestsLeft);
            }

            if (((int) syncVarDirtyBits & 4) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                SyncListInt.WriteInstance(writer, m_cinematicsPlayedThisMatch);
            }

            if (!flag)
                writer.WritePackedUInt32(syncVarDirtyBits);
            return flag;
        }

        public override void OnDeserialize(NetworkReader reader, bool initialState)
        {
            if (initialState)
            {
                SyncListBool.ReadReference(reader, m_abilityRequested);
                m_numCinematicRequestsLeft = (int) reader.ReadPackedUInt32();
                SyncListInt.ReadReference(reader, m_cinematicsPlayedThisMatch);
            }
            else
            {
                int num = (int) reader.ReadPackedUInt32();
                if ((num & 1) != 0)
                    SyncListBool.ReadReference(reader, m_abilityRequested);
                if ((num & 2) != 0)
                    m_numCinematicRequestsLeft = (int) reader.ReadPackedUInt32();
                if ((num & 4) == 0)
                    return;
                SyncListInt.ReadReference(reader, m_cinematicsPlayedThisMatch);
            }
        }

        public override string ToString()
        {
            return $"{nameof(ActorCinematicRequests)}>(" +
                   ")";
        }
    }
}
