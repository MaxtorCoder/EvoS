using System;
using System.Collections.Generic;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.NetworkBehaviours
{
    [Serializable]
    [SerializedMonoBehaviour("PowerUp")]
    public class PowerUp : NetworkBehaviour
    {
        public SerializedComponent m_ability;
        public string m_powerUpName;
        public string m_powerUpToolTip;
        public string m_audioEventPickUp = "ui_pickup_health";
        public SerializedComponent m_sequencePrefab;
        public bool m_restrictPickupByTeam;

        public PowerUpCategory m_chatterCategory;

//  [SyncVar]
        public bool m_isSpoil;

//  [SyncVar]
        public bool m_ignoreSpawnSplineForSequence;
        public int m_duration;

        private static int kRpcRpcOnPickedUp = -430057904;

//  [SyncVar]
        private Team m_pickupTeam = Team.Objects;

//  [SyncVar(hook = "HookSetGuid")]
        private int m_guid = -1;
        private List<int> m_clientSequenceIds = new List<int>();
        private static int s_nextPowerupGuid;

//  [SyncVar]
        private uint m_sequenceSourceId;

//  private BoardSquare m_boardSquare;

        private int m_age;
        private bool m_pickedUp;

        private bool m_stolen;

//  private ActorTag m_tags;
        private bool m_markedForRemoval;

//  private SequenceSource _sequenceSource;
        private static int kRpcRpcOnSteal;

        static PowerUp()
        {
//    NetworkBehaviour.RegisterRpcDelegate(typeof (PowerUp), PowerUp.kRpcRpcOnPickedUp, new NetworkBehaviour.CmdDelegate(PowerUp.InvokeRpcRpcOnPickedUp));
//    PowerUp.kRpcRpcOnSteal = 1919536730;
//    NetworkBehaviour.RegisterRpcDelegate(typeof (PowerUp), PowerUp.kRpcRpcOnSteal, new NetworkBehaviour.CmdDelegate(PowerUp.InvokeRpcRpcOnSteal));
//    NetworkCRC.RegisterBehaviour(nameof (PowerUp), 0);
        }

        public PowerUp()
        {
        }

        public PowerUp(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_ability = new SerializedComponent(assetFile, stream);
            m_powerUpName = stream.ReadString32();
            m_powerUpToolTip = stream.ReadString32();
            m_audioEventPickUp = stream.ReadString32();
            m_sequencePrefab = new SerializedComponent(assetFile, stream);
            m_restrictPickupByTeam = stream.ReadBoolean();
            stream.AlignTo();
            m_chatterCategory = (PowerUpCategory) stream.ReadInt32();
            m_isSpoil = stream.ReadBoolean();
            stream.AlignTo();
            m_ignoreSpawnSplineForSequence = stream.ReadBoolean();
            stream.AlignTo();
            m_duration = stream.ReadInt32();
        }

        public override string ToString()
        {
            return $"{nameof(PowerUp)}>(" +
                   $"{nameof(m_ability)}: {m_ability}, " +
                   $"{nameof(m_powerUpName)}: {m_powerUpName}, " +
                   $"{nameof(m_powerUpToolTip)}: {m_powerUpToolTip}, " +
                   $"{nameof(m_audioEventPickUp)}: {m_audioEventPickUp}, " +
                   $"{nameof(m_sequencePrefab)}: {m_sequencePrefab}, " +
                   $"{nameof(m_restrictPickupByTeam)}: {m_restrictPickupByTeam}, " +
                   $"{nameof(m_chatterCategory)}: {m_chatterCategory}, " +
                   $"{nameof(m_isSpoil)}: {m_isSpoil}, " +
                   $"{nameof(m_ignoreSpawnSplineForSequence)}: {m_ignoreSpawnSplineForSequence}, " +
                   $"{nameof(m_duration)}: {m_duration}, " +
                   ")";
        }

        public enum PowerUpCategory
        {
            NoCategory,
            Healing,
            Might,
            Movement,
            Energy,
        }
    }
}
