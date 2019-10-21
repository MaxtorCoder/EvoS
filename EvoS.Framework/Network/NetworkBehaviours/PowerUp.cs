using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Game;
using EvoS.Framework.Misc;
using EvoS.Framework.Network.Static;
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
        private BoardSquare m_boardSquare;
        private int m_age;
        private bool m_pickedUp;
        private bool m_stolen;
        private ActorTag m_tags;
        private bool m_markedForRemoval;
        private SequenceSource _sequenceSource;
        private static int kRpcRpcOnSteal;

        public Team Networkm_pickupTeam
        {
            get => m_pickupTeam;
            [param: In] set => SetSyncVar(value, ref m_pickupTeam, 1U);
        }

        public int Networkm_guid
        {
            get => m_guid;
            [param: In]
            set
            {
                int num = value;
                ref int local = ref m_guid;
                if (EvoSGameConfig.NetworkIsClient && !syncVarHookGuard)
                {
                    syncVarHookGuard = true;
                    HookSetGuid(value);
                    syncVarHookGuard = false;
                }

                SetSyncVar(num, ref local, 2U);
            }
        }

        public uint Networkm_sequenceSourceId
        {
            get => m_sequenceSourceId;
            [param: In] set => SetSyncVar(value, ref m_sequenceSourceId, 4U);
        }

        public bool Networkm_isSpoil
        {
            get => m_isSpoil;
            [param: In] set => SetSyncVar(value, ref m_isSpoil, 8U);
        }

        public bool Networkm_ignoreSpawnSplineForSequence
        {
            get => m_ignoreSpawnSplineForSequence;
            [param: In] set => SetSyncVar(value, ref m_ignoreSpawnSplineForSequence, 16U);
        }

        public SequenceSource SequenceSource
        {
            get
            {
                if (_sequenceSource == null)
                {
                    _sequenceSource = new SequenceSource(null, null, false);
                }

                return _sequenceSource;
            }
        }

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

        private void HookSetGuid(int guid)
        {
            Networkm_guid = guid;
//            PowerUpManager.Get().SetPowerUpGuid(this, guid);
        }

        public override void Awake()
        {
            if (!EvoSGameConfig.NetworkIsServer)
                return;
            Networkm_guid = s_nextPowerupGuid++;
            Networkm_sequenceSourceId = SequenceSource.RootID;
        }

        public override bool OnSerialize(NetworkWriter writer, bool forceAll)
        {
            if (forceAll)
            {
                writer.Write((int) m_pickupTeam);
                writer.WritePackedUInt32((uint) m_guid);
                writer.WritePackedUInt32(m_sequenceSourceId);
                writer.Write(m_isSpoil);
                writer.Write(m_ignoreSpawnSplineForSequence);
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

                writer.Write((int) m_pickupTeam);
            }

            if (((int) syncVarDirtyBits & 2) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.WritePackedUInt32((uint) m_guid);
            }

            if (((int) syncVarDirtyBits & 4) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.WritePackedUInt32(m_sequenceSourceId);
            }

            if (((int) syncVarDirtyBits & 8) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.Write(m_isSpoil);
            }

            if (((int) syncVarDirtyBits & 16) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.Write(m_ignoreSpawnSplineForSequence);
            }

            if (!flag)
                writer.WritePackedUInt32(syncVarDirtyBits);
            return flag;
        }

        public override void OnDeserialize(NetworkReader reader, bool initialState)
        {
            if (initialState)
            {
                m_pickupTeam = (Team) reader.ReadInt32();
                m_guid = (int) reader.ReadPackedUInt32();
                m_sequenceSourceId = reader.ReadPackedUInt32();
                m_isSpoil = reader.ReadBoolean();
                m_ignoreSpawnSplineForSequence = reader.ReadBoolean();
            }
            else
            {
                int num = (int) reader.ReadPackedUInt32();
                if ((num & 1) != 0)
                    m_pickupTeam = (Team) reader.ReadInt32();
                if ((num & 2) != 0)
                    HookSetGuid((int) reader.ReadPackedUInt32());
                if ((num & 4) != 0)
                    m_sequenceSourceId = reader.ReadPackedUInt32();
                if ((num & 8) != 0)
                    m_isSpoil = reader.ReadBoolean();
                if ((num & 16) == 0)
                    return;
                m_ignoreSpawnSplineForSequence = reader.ReadBoolean();
            }
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
            Energy
        }
    }
}
