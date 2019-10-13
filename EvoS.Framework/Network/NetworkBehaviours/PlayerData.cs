using System;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.Unity;
using Newtonsoft.Json;

namespace EvoS.Framework.Network.NetworkBehaviours
{
    [Serializable]
    [SerializedMonoBehaviour("PlayerData")]
    public class PlayerData : NetworkBehaviour
    {
        private static int kCmdCmdTutorialQueueEmpty = -1356677979;
        private static int kCmdCmdDebugEndGame = -1295019579;
        private static int kCmdCmdSetPausedForDebugging = -1571708758;
        private static int kCmdCmdTheatricsManagerUpdatePhaseEnded = -438226347;
        public static int s_invalidPlayerIndex = -1;
        public string m_playerHandle = string.Empty;
        private int m_playerIndex = s_invalidPlayerIndex;
        public bool m_reconnecting;
        public bool m_isLocal;
        [JsonIgnore]
        public ActorData ActorData;
        public SerializedComponent SerializedActorData;
        public Player m_player;

        public int PlayerIndex => m_playerIndex;
//        private FogOfWar m_fogOfWar;

        public void Awake()
        {
            ActorData = GetComponent<ActorData>();
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            SerializedActorData = new SerializedComponent(assetFile, stream); // class ActorData
        }

        public override bool OnSerialize(NetworkWriter writer, bool initialState)
        {
            var networkWriterAdapter = new NetworkWriterAdapter(writer);
            networkWriterAdapter.Serialize(ref m_playerHandle);
            networkWriterAdapter.Serialize(ref m_playerIndex);
            m_player.OnSerializeHelper(networkWriterAdapter);
            return true;
        }

        public override void OnDeserialize(NetworkReader reader, bool initialState)
        {
            var networkReaderAdapter = new NetworkReaderAdapter(reader);
            networkReaderAdapter.Serialize(ref m_playerHandle);
            networkReaderAdapter.Serialize(ref m_playerIndex);
            m_player.OnSerializeHelper(networkReaderAdapter);
        }

        public override string ToString()
        {
            return $"{nameof(PlayerData)}>(" +
                   $"{nameof(ActorData)}: {ActorData}, " +
                   ")";
        }
    }
}
