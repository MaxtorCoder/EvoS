using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Game;
using EvoS.Framework.Misc;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.NetworkBehaviours
{
    [Serializable]
    [SerializedMonoBehaviour("PowerUpSpawner")]
    public class PowerUpSpawner : NetworkBehaviour
    {
        public SerializedComponent m_powerUpPrefab;
        public SerializedComponent m_baseSequencePrefab;

        public SerializedComponent m_spawnSequencePrefab;

//        [Separator("Additional Prefabs for mixing up powerup to spawn", true)]
        public ExtraPowerupSelectMode m_extraPowerupSelectMode;
        public SerializedVector<SerializedComponent> m_extraPowerupsForMixedSpawn;

        public bool m_useSameFirstPowerupIfRandom = true;

//        [Separator("Timing of spawns", true)]
        public int m_spawnInterval;
        public int m_initialSpawnDelay;
        public Team m_teamRestriction;
        public SerializedVector<string> m_tagsToApplyToPowerup;

        private int m_lastServerSpawnPrefabIndex = -1;

        private int m_currentBasePrefabIndex = -1;

//        [SyncVar]
        private bool m_spawningEnabled = true;
        private bool m_isReady = true;
        private BoardSquare m_boardSquare;

        private List<PowerupSpawnInfo> m_finalizedPowerupSpawnInfoList;

//        [SyncVar]
        private uint m_sequenceSourceId;

//        private Sequence[] m_baseSequences;
//        private Sequence[] m_spawnSequences;
//        [SyncVar]
        private int m_nextPowerupPrefabIndex;

//        [SyncVar(hook = "HookNextSpawnTurn")]
        private int m_nextSpawnTurn;
        private PowerUp m_powerUpInstance;
        private bool m_initialized;

        public PowerUpSpawner()
        {
        }

        public PowerUpSpawner(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public int Networkm_nextSpawnTurn
        {
            get => m_nextSpawnTurn;
            [param: In]
            set
            {
                int num = value;
                ref int local = ref m_nextSpawnTurn;
                if (EvoSGameConfig.NetworkIsClient && !syncVarHookGuard)
                {
                    syncVarHookGuard = true;
                    HookNextSpawnTurn(value);
                    syncVarHookGuard = false;
                }

                SetSyncVar(num, ref local, 4U);
            }
        }

        private void HookNextSpawnTurn(int nextSpawnTurn)
        {
            bool flag = m_nextSpawnTurn != nextSpawnTurn;
            Networkm_nextSpawnTurn = nextSpawnTurn;
            if (!flag)
                return;
//          this.UpdateTimerController();
        }

        public override bool OnSerialize(NetworkWriter writer, bool forceAll)
        {
            if (forceAll)
            {
                writer.WritePackedUInt32(m_sequenceSourceId);
                writer.WritePackedUInt32((uint) m_nextPowerupPrefabIndex);
                writer.WritePackedUInt32((uint) m_nextSpawnTurn);
                writer.Write(m_spawningEnabled);
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

                writer.WritePackedUInt32(m_sequenceSourceId);
            }

            if (((int) syncVarDirtyBits & 2) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.WritePackedUInt32((uint) m_nextPowerupPrefabIndex);
            }

            if (((int) syncVarDirtyBits & 4) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.WritePackedUInt32((uint) m_nextSpawnTurn);
            }

            if (((int) syncVarDirtyBits & 8) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.Write(m_spawningEnabled);
            }

            if (!flag)
                writer.WritePackedUInt32(syncVarDirtyBits);
            return flag;
        }

        public override void OnDeserialize(NetworkReader reader, bool initialState)
        {
            if (initialState)
            {
                m_sequenceSourceId = reader.ReadPackedUInt32();
                m_nextPowerupPrefabIndex = (int) reader.ReadPackedUInt32();
                m_nextSpawnTurn = (int) reader.ReadPackedUInt32();
                m_spawningEnabled = reader.ReadBoolean();
            }
            else
            {
                int num = (int) reader.ReadPackedUInt32();
                if ((num & 1) != 0)
                    m_sequenceSourceId = reader.ReadPackedUInt32();
                if ((num & 2) != 0)
                    m_nextPowerupPrefabIndex = (int) reader.ReadPackedUInt32();
                if ((num & 4) != 0)
                    HookNextSpawnTurn((int) reader.ReadPackedUInt32());
                if ((num & 8) == 0)
                    return;
                m_spawningEnabled = reader.ReadBoolean();
            }
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_powerUpPrefab = new SerializedComponent(assetFile, stream);
            m_baseSequencePrefab = new SerializedComponent(assetFile, stream);
            m_spawnSequencePrefab = new SerializedComponent(assetFile, stream);
            m_extraPowerupSelectMode = (ExtraPowerupSelectMode) stream.ReadInt32();
            m_extraPowerupsForMixedSpawn = new SerializedVector<SerializedComponent>(assetFile, stream);
            m_useSameFirstPowerupIfRandom = stream.ReadBoolean();
            stream.AlignTo();
            m_spawnInterval = stream.ReadInt32();
            m_initialSpawnDelay = stream.ReadInt32();
            m_teamRestriction = (Team) stream.ReadInt32();
            m_tagsToApplyToPowerup = new SerializedVector<string>();
        }

        public override string ToString()
        {
            return $"{nameof(PowerUpSpawner)}>(" +
                   $"{nameof(m_powerUpPrefab)}: {m_powerUpPrefab}, " +
                   $"{nameof(m_baseSequencePrefab)}: {m_baseSequencePrefab}, " +
                   $"{nameof(m_spawnSequencePrefab)}: {m_spawnSequencePrefab}, " +
                   $"{nameof(m_extraPowerupSelectMode)}: {m_extraPowerupSelectMode}, " +
                   $"{nameof(m_extraPowerupsForMixedSpawn)}: {m_extraPowerupsForMixedSpawn}, " +
                   $"{nameof(m_useSameFirstPowerupIfRandom)}: {m_useSameFirstPowerupIfRandom}, " +
                   $"{nameof(m_spawnInterval)}: {m_spawnInterval}, " +
                   $"{nameof(m_initialSpawnDelay)}: {m_initialSpawnDelay}, " +
                   $"{nameof(m_teamRestriction)}: {m_teamRestriction}, " +
                   $"{nameof(m_tagsToApplyToPowerup)}: {m_tagsToApplyToPowerup}, " +
                   ")";
        }

        [Serializable]
        public class PowerupSpawnInfo : ISerializedItem
        {
            public SerializedComponent m_powerupObjectPrefab;
            public SerializedComponent m_baseSeqPrefab;
            public SerializedComponent m_spawnSeqPrefab;

            public void DeserializeAsset(AssetFile assetFile, StreamReader stream)
            {
                m_powerupObjectPrefab = new SerializedComponent(assetFile, stream);
                m_baseSeqPrefab = new SerializedComponent(assetFile, stream);
                m_spawnSeqPrefab = new SerializedComponent(assetFile, stream);
            }

            public override string ToString()
            {
                return $"{nameof(PowerupSpawnInfo)}(" +
                       $"{nameof(m_powerupObjectPrefab)}: {m_powerupObjectPrefab}, " +
                       $"{nameof(m_baseSeqPrefab)}: {m_baseSeqPrefab}, " +
                       $"{nameof(m_spawnSeqPrefab)}: {m_spawnSeqPrefab}" +
                       ")";
            }
        }

        public enum ExtraPowerupSelectMode
        {
            InOrder,
            Random
        }
    }
}
