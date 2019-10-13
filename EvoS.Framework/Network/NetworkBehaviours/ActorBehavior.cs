using System;
using System.Collections.Generic;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Game;
using EvoS.Framework.Logging;
using EvoS.Framework.Network.Unity;
using Newtonsoft.Json;

namespace EvoS.Framework.Network.NetworkBehaviours
{
    [Serializable]
    [SerializedMonoBehaviour("ActorBehavior")]
    public class ActorBehavior : NetworkBehaviour
    {
        private static int kListm_syncEnemySourcesForDamageOrDebuff = -1894796663;
        private SyncListUInt m_syncEnemySourcesForDamageOrDebuff = new SyncListUInt();
        private SyncListUInt m_syncAllySourcesForHealAndBuff = new SyncListUInt();
        private List<int> m_clientEffectSourceActors = new List<int>();
        private List<int> m_clientDamageSourceActors = new List<int>();

        private List<int> m_clientHealSourceActors = new List<int>();

//  [SyncVar]
        private short m_totalDeaths;

//  [SyncVar]
        private short m_totalPlayerKills;

//  [SyncVar]
        private short m_totalPlayerAssists;

        private short m_totalMinionKills;

//  [SyncVar]
        private int m_totalPlayerDamage;

//  [SyncVar]
        private int m_totalPlayerHealing;

//  [SyncVar]
        private int m_totalPlayerHealingFromAbility;

//  [SyncVar]
        private int m_totalPlayerOverheal;

//  [SyncVar]
        private int m_totalPlayerAbsorb;

//  [SyncVar]
        private int m_totalPlayerPotentialAbsorb;

//  [SyncVar]
        private int m_totalEnergyGained;

//  [SyncVar]
        private int m_totalPlayerDamageReceived;

//  [SyncVar]
        private int m_totalPlayerHealingReceived;

//  [SyncVar]
        private int m_totalPlayerAbsorbReceived;

//  [SyncVar]
        private float m_totalPlayerLockInTime;

//  [SyncVar]
        private int m_totalPlayerTurns;

//  [SyncVar]
        private int m_damageDodgedByEvades;

//  [SyncVar]
        private int m_damageInterceptedByEvades;

//  [SyncVar]
        private int m_myIncomingDamageReducedByCover;

//  [SyncVar]
        private int m_myOutgoingDamageReducedByCover;

//  [SyncVar]
        private int m_myIncomingOverkillDamageTaken;

//  [SyncVar]
        private int m_myOutgoingOverkillDamageDealt;

//  [SyncVar]
        private int m_myOutgoingExtraDamageFromEmpowered;

//  [SyncVar]
        private int m_myOutgoingDamageReducedFromWeakened;
        private int m_myOutgoingExtraDamageFromTargetsVulnerable;
        private int m_myOutgoingDamageReducedFromTargetsArmored;
        private int m_myIncomingExtraDamageFromCastersEmpowered;
        private int m_myIncomingDamageReducedFromCastersWeakened;
        private int m_myIncomingExtraDamageIncreasedByVulnerable;

        private int m_myIncomingDamageReducedByArmored;

//  [SyncVar]
        private int m_teamOutgoingDamageIncreasedByEmpoweredFromMe;

//  [SyncVar]
        private int m_teamIncomingDamageReducedByWeakenedFromMe;
        private int m_teamIncomingDamageReducedByArmoredFromMe;

        private int m_teamOutgoingDamageIncreasedByVulnerableFromMe;

//  [SyncVar]
        private int m_teamExtraEnergyGainFromMe;

//  [SyncVar]
        private float m_movementDeniedByMe;

//  [SyncVar]
        private int m_totalEnemySighted;
        private ActorData m_actor;
        private int m_totalDeathsOnTurnStart;
        private int m_serverIncomingDamageReducedByCoverThisTurn;
        public const string c_debugHeader = "<color=magenta>ActorBehavior: </color>";
        private static int kListm_syncAllySourcesForHealAndBuff;

        public SyncListUInt SyncEnemySourcesForDamageOrDebuff => m_syncEnemySourcesForDamageOrDebuff;
        public SyncListUInt SyncAllySourcesForHealAndBuff => m_syncAllySourcesForHealAndBuff;
        public List<int> ClientEffectSourceActors => m_clientEffectSourceActors;
        public List<int> ClientDamageSourceActors => m_clientDamageSourceActors;
        public List<int> ClientHealSourceActors => m_clientHealSourceActors;
        public short TotalDeaths => m_totalDeaths;
        public short TotalPlayerKills => m_totalPlayerKills;
        public short TotalPlayerAssists => m_totalPlayerAssists;
        public short TotalMinionKills => m_totalMinionKills;
        public int TotalPlayerDamage => m_totalPlayerDamage;
        public int TotalPlayerHealing => m_totalPlayerHealing;
        public int TotalPlayerHealingFromAbility => m_totalPlayerHealingFromAbility;
        public int TotalPlayerOverheal => m_totalPlayerOverheal;
        public int TotalPlayerAbsorb => m_totalPlayerAbsorb;
        public int TotalPlayerPotentialAbsorb => m_totalPlayerPotentialAbsorb;
        public int TotalEnergyGained => m_totalEnergyGained;
        public int TotalPlayerDamageReceived => m_totalPlayerDamageReceived;
        public int TotalPlayerHealingReceived => m_totalPlayerHealingReceived;
        public int TotalPlayerAbsorbReceived => m_totalPlayerAbsorbReceived;
        public float TotalPlayerLockInTime => m_totalPlayerLockInTime;
        public int TotalPlayerTurns => m_totalPlayerTurns;
        public int DamageDodgedByEvades => m_damageDodgedByEvades;
        public int DamageInterceptedByEvades => m_damageInterceptedByEvades;
        public int MyIncomingDamageReducedByCover => m_myIncomingDamageReducedByCover;
        public int MyOutgoingDamageReducedByCover => m_myOutgoingDamageReducedByCover;
        public int MyIncomingOverkillDamageTaken => m_myIncomingOverkillDamageTaken;
        public int MyOutgoingOverkillDamageDealt => m_myOutgoingOverkillDamageDealt;
        public int MyOutgoingExtraDamageFromEmpowered => m_myOutgoingExtraDamageFromEmpowered;
        public int MyOutgoingDamageReducedFromWeakened => m_myOutgoingDamageReducedFromWeakened;
        public int MyOutgoingExtraDamageFromTargetsVulnerable => m_myOutgoingExtraDamageFromTargetsVulnerable;
        public int MyOutgoingDamageReducedFromTargetsArmored => m_myOutgoingDamageReducedFromTargetsArmored;
        public int MyIncomingExtraDamageFromCastersEmpowered => m_myIncomingExtraDamageFromCastersEmpowered;
        public int MyIncomingDamageReducedFromCastersWeakened => m_myIncomingDamageReducedFromCastersWeakened;
        public int MyIncomingExtraDamageIncreasedByVulnerable => m_myIncomingExtraDamageIncreasedByVulnerable;
        public int MyIncomingDamageReducedByArmored => m_myIncomingDamageReducedByArmored;
        public int TeamOutgoingDamageIncreasedByEmpoweredFromMe => m_teamOutgoingDamageIncreasedByEmpoweredFromMe;
        public int TeamIncomingDamageReducedByWeakenedFromMe => m_teamIncomingDamageReducedByWeakenedFromMe;
        public int TeamIncomingDamageReducedByArmoredFromMe => m_teamIncomingDamageReducedByArmoredFromMe;
        public int TeamOutgoingDamageIncreasedByVulnerableFromMe => m_teamOutgoingDamageIncreasedByVulnerableFromMe;
        public int TeamExtraEnergyGainFromMe => m_teamExtraEnergyGainFromMe;
        public float MovementDeniedByMe => m_movementDeniedByMe;
        public int TotalEnemySighted => m_totalEnemySighted;
        [JsonIgnore]
        public ActorData Actor => m_actor;
        public int TotalDeathsOnTurnStart => m_totalDeathsOnTurnStart;
        public int ServerIncomingDamageReducedByCoverThisTurn => m_serverIncomingDamageReducedByCoverThisTurn;

        static ActorBehavior()
        {
            RegisterSyncListDelegate(typeof(ActorBehavior),
                kListm_syncEnemySourcesForDamageOrDebuff,
                InvokeSyncListm_syncEnemySourcesForDamageOrDebuff);
            kListm_syncAllySourcesForHealAndBuff = 1807084515;
            RegisterSyncListDelegate(typeof(ActorBehavior),
                kListm_syncAllySourcesForHealAndBuff,
                InvokeSyncListm_syncAllySourcesForHealAndBuff);
        }

        public ActorBehavior()
        {
        }

        public ActorBehavior(AssetFile assetFile, StreamReader stream)
        {
            DeserializeAsset(assetFile, stream);
        }

        public override void Awake()
        {
            m_actor = GetComponent<ActorData>();
            m_syncEnemySourcesForDamageOrDebuff.InitializeBehaviour(this, kListm_syncEnemySourcesForDamageOrDebuff);
            m_syncAllySourcesForHealAndBuff.InitializeBehaviour(this, kListm_syncAllySourcesForHealAndBuff);
        }

        protected static void InvokeSyncListm_syncEnemySourcesForDamageOrDebuff(
            NetworkBehaviour obj,
            NetworkReader reader)
        {
            if (!EvoSGameConfig.NetworkIsClient)
                Log.Print(LogType.Error, "SyncList m_syncEnemySourcesForDamageOrDebuff called on server.");
            else
                ((ActorBehavior) obj).m_syncEnemySourcesForDamageOrDebuff.HandleMsg(reader);
        }

        protected static void InvokeSyncListm_syncAllySourcesForHealAndBuff(
            NetworkBehaviour obj,
            NetworkReader reader)
        {
            if (!EvoSGameConfig.NetworkIsClient)
                Log.Print(LogType.Error, "SyncList m_syncAllySourcesForHealAndBuff called on server.");
            else
                ((ActorBehavior) obj).m_syncAllySourcesForHealAndBuff.HandleMsg(reader);
        }

        public override bool OnSerialize(NetworkWriter writer, bool forceAll)
        {
            if (forceAll)
            {
                SyncListUInt.WriteInstance(writer, m_syncEnemySourcesForDamageOrDebuff);
                SyncListUInt.WriteInstance(writer, m_syncAllySourcesForHealAndBuff);
                writer.WritePackedUInt32((uint) m_totalDeaths);
                writer.WritePackedUInt32((uint) m_totalPlayerKills);
                writer.WritePackedUInt32((uint) m_totalPlayerAssists);
                writer.WritePackedUInt32((uint) m_totalPlayerDamage);
                writer.WritePackedUInt32((uint) m_totalPlayerHealing);
                writer.WritePackedUInt32((uint) m_totalPlayerHealingFromAbility);
                writer.WritePackedUInt32((uint) m_totalPlayerOverheal);
                writer.WritePackedUInt32((uint) m_totalPlayerAbsorb);
                writer.WritePackedUInt32((uint) m_totalPlayerPotentialAbsorb);
                writer.WritePackedUInt32((uint) m_totalEnergyGained);
                writer.WritePackedUInt32((uint) m_totalPlayerDamageReceived);
                writer.WritePackedUInt32((uint) m_totalPlayerHealingReceived);
                writer.WritePackedUInt32((uint) m_totalPlayerAbsorbReceived);
                writer.Write(m_totalPlayerLockInTime);
                writer.WritePackedUInt32((uint) m_totalPlayerTurns);
                writer.WritePackedUInt32((uint) m_damageDodgedByEvades);
                writer.WritePackedUInt32((uint) m_damageInterceptedByEvades);
                writer.WritePackedUInt32((uint) m_myIncomingDamageReducedByCover);
                writer.WritePackedUInt32((uint) m_myOutgoingDamageReducedByCover);
                writer.WritePackedUInt32((uint) m_myIncomingOverkillDamageTaken);
                writer.WritePackedUInt32((uint) m_myOutgoingOverkillDamageDealt);
                writer.WritePackedUInt32((uint) m_myOutgoingExtraDamageFromEmpowered);
                writer.WritePackedUInt32((uint) m_myOutgoingDamageReducedFromWeakened);
                writer.WritePackedUInt32((uint) m_teamOutgoingDamageIncreasedByEmpoweredFromMe);
                writer.WritePackedUInt32((uint) m_teamIncomingDamageReducedByWeakenedFromMe);
                writer.WritePackedUInt32((uint) m_teamExtraEnergyGainFromMe);
                writer.Write(m_movementDeniedByMe);
                writer.WritePackedUInt32((uint) m_totalEnemySighted);
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

                SyncListUInt.WriteInstance(writer, m_syncEnemySourcesForDamageOrDebuff);
            }

            if (((int) syncVarDirtyBits & 2) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                SyncListUInt.WriteInstance(writer, m_syncAllySourcesForHealAndBuff);
            }

            if (((int) syncVarDirtyBits & 4) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.WritePackedUInt32((uint) m_totalDeaths);
            }

            if (((int) syncVarDirtyBits & 8) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.WritePackedUInt32((uint) m_totalPlayerKills);
            }

            if (((int) syncVarDirtyBits & 16) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.WritePackedUInt32((uint) m_totalPlayerAssists);
            }

            if (((int) syncVarDirtyBits & 32) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.WritePackedUInt32((uint) m_totalPlayerDamage);
            }

            if (((int) syncVarDirtyBits & 64) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.WritePackedUInt32((uint) m_totalPlayerHealing);
            }

            if (((int) syncVarDirtyBits & 128) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.WritePackedUInt32((uint) m_totalPlayerHealingFromAbility);
            }

            if (((int) syncVarDirtyBits & 256) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.WritePackedUInt32((uint) m_totalPlayerOverheal);
            }

            if (((int) syncVarDirtyBits & 512) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.WritePackedUInt32((uint) m_totalPlayerAbsorb);
            }

            if (((int) syncVarDirtyBits & 1024) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.WritePackedUInt32((uint) m_totalPlayerPotentialAbsorb);
            }

            if (((int) syncVarDirtyBits & 2048) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.WritePackedUInt32((uint) m_totalEnergyGained);
            }

            if (((int) syncVarDirtyBits & 4096) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.WritePackedUInt32((uint) m_totalPlayerDamageReceived);
            }

            if (((int) syncVarDirtyBits & 8192) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.WritePackedUInt32((uint) m_totalPlayerHealingReceived);
            }

            if (((int) syncVarDirtyBits & 16384) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.WritePackedUInt32((uint) m_totalPlayerAbsorbReceived);
            }

            if (((int) syncVarDirtyBits & 32768) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.Write(m_totalPlayerLockInTime);
            }

            if (((int) syncVarDirtyBits & 65536) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.WritePackedUInt32((uint) m_totalPlayerTurns);
            }

            if (((int) syncVarDirtyBits & 131072) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.WritePackedUInt32((uint) m_damageDodgedByEvades);
            }

            if (((int) syncVarDirtyBits & 262144) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.WritePackedUInt32((uint) m_damageInterceptedByEvades);
            }

            if (((int) syncVarDirtyBits & 524288) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.WritePackedUInt32((uint) m_myIncomingDamageReducedByCover);
            }

            if (((int) syncVarDirtyBits & 1048576) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.WritePackedUInt32((uint) m_myOutgoingDamageReducedByCover);
            }

            if (((int) syncVarDirtyBits & 2097152) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.WritePackedUInt32((uint) m_myIncomingOverkillDamageTaken);
            }

            if (((int) syncVarDirtyBits & 4194304) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.WritePackedUInt32((uint) m_myOutgoingOverkillDamageDealt);
            }

            if (((int) syncVarDirtyBits & 8388608) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.WritePackedUInt32((uint) m_myOutgoingExtraDamageFromEmpowered);
            }

            if (((int) syncVarDirtyBits & 16777216) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.WritePackedUInt32((uint) m_myOutgoingDamageReducedFromWeakened);
            }

            if (((int) syncVarDirtyBits & 33554432) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.WritePackedUInt32((uint) m_teamOutgoingDamageIncreasedByEmpoweredFromMe);
            }

            if (((int) syncVarDirtyBits & 67108864) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.WritePackedUInt32((uint) m_teamIncomingDamageReducedByWeakenedFromMe);
            }

            if (((int) syncVarDirtyBits & 134217728) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.WritePackedUInt32((uint) m_teamExtraEnergyGainFromMe);
            }

            if (((int) syncVarDirtyBits & 268435456) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.Write(m_movementDeniedByMe);
            }

            if (((int) syncVarDirtyBits & 536870912) != 0)
            {
                if (!flag)
                {
                    writer.WritePackedUInt32(syncVarDirtyBits);
                    flag = true;
                }

                writer.WritePackedUInt32((uint) m_totalEnemySighted);
            }

            if (!flag)
                writer.WritePackedUInt32(syncVarDirtyBits);
            return flag;
        }

        public override void OnDeserialize(NetworkReader reader, bool initialState)
        {
            if (initialState)
            {
                SyncListUInt.ReadReference(reader, m_syncEnemySourcesForDamageOrDebuff);
                SyncListUInt.ReadReference(reader, m_syncAllySourcesForHealAndBuff);
                m_totalDeaths = (short) reader.ReadPackedUInt32();
                m_totalPlayerKills = (short) reader.ReadPackedUInt32();
                m_totalPlayerAssists = (short) reader.ReadPackedUInt32();
                m_totalPlayerDamage = (int) reader.ReadPackedUInt32();
                m_totalPlayerHealing = (int) reader.ReadPackedUInt32();
                m_totalPlayerHealingFromAbility = (int) reader.ReadPackedUInt32();
                m_totalPlayerOverheal = (int) reader.ReadPackedUInt32();
                m_totalPlayerAbsorb = (int) reader.ReadPackedUInt32();
                m_totalPlayerPotentialAbsorb = (int) reader.ReadPackedUInt32();
                m_totalEnergyGained = (int) reader.ReadPackedUInt32();
                m_totalPlayerDamageReceived = (int) reader.ReadPackedUInt32();
                m_totalPlayerHealingReceived = (int) reader.ReadPackedUInt32();
                m_totalPlayerAbsorbReceived = (int) reader.ReadPackedUInt32();
                m_totalPlayerLockInTime = reader.ReadSingle();
                m_totalPlayerTurns = (int) reader.ReadPackedUInt32();
                m_damageDodgedByEvades = (int) reader.ReadPackedUInt32();
                m_damageInterceptedByEvades = (int) reader.ReadPackedUInt32();
                m_myIncomingDamageReducedByCover = (int) reader.ReadPackedUInt32();
                m_myOutgoingDamageReducedByCover = (int) reader.ReadPackedUInt32();
                m_myIncomingOverkillDamageTaken = (int) reader.ReadPackedUInt32();
                m_myOutgoingOverkillDamageDealt = (int) reader.ReadPackedUInt32();
                m_myOutgoingExtraDamageFromEmpowered = (int) reader.ReadPackedUInt32();
                m_myOutgoingDamageReducedFromWeakened = (int) reader.ReadPackedUInt32();
                m_teamOutgoingDamageIncreasedByEmpoweredFromMe = (int) reader.ReadPackedUInt32();
                m_teamIncomingDamageReducedByWeakenedFromMe = (int) reader.ReadPackedUInt32();
                m_teamExtraEnergyGainFromMe = (int) reader.ReadPackedUInt32();
                m_movementDeniedByMe = reader.ReadSingle();
                m_totalEnemySighted = (int) reader.ReadPackedUInt32();
            }
            else
            {
                int num = (int) reader.ReadPackedUInt32();
                if ((num & 1) != 0)
                    SyncListUInt.ReadReference(reader, m_syncEnemySourcesForDamageOrDebuff);
                if ((num & 2) != 0)
                    SyncListUInt.ReadReference(reader, m_syncAllySourcesForHealAndBuff);
                if ((num & 4) != 0)
                    m_totalDeaths = (short) reader.ReadPackedUInt32();
                if ((num & 8) != 0)
                    m_totalPlayerKills = (short) reader.ReadPackedUInt32();
                if ((num & 16) != 0)
                    m_totalPlayerAssists = (short) reader.ReadPackedUInt32();
                if ((num & 32) != 0)
                    m_totalPlayerDamage = (int) reader.ReadPackedUInt32();
                if ((num & 64) != 0)
                    m_totalPlayerHealing = (int) reader.ReadPackedUInt32();
                if ((num & 128) != 0)
                    m_totalPlayerHealingFromAbility = (int) reader.ReadPackedUInt32();
                if ((num & 256) != 0)
                    m_totalPlayerOverheal = (int) reader.ReadPackedUInt32();
                if ((num & 512) != 0)
                    m_totalPlayerAbsorb = (int) reader.ReadPackedUInt32();
                if ((num & 1024) != 0)
                    m_totalPlayerPotentialAbsorb = (int) reader.ReadPackedUInt32();
                if ((num & 2048) != 0)
                    m_totalEnergyGained = (int) reader.ReadPackedUInt32();
                if ((num & 4096) != 0)
                    m_totalPlayerDamageReceived = (int) reader.ReadPackedUInt32();
                if ((num & 8192) != 0)
                    m_totalPlayerHealingReceived = (int) reader.ReadPackedUInt32();
                if ((num & 16384) != 0)
                    m_totalPlayerAbsorbReceived = (int) reader.ReadPackedUInt32();
                if ((num & 32768) != 0)
                    m_totalPlayerLockInTime = reader.ReadSingle();
                if ((num & 65536) != 0)
                    m_totalPlayerTurns = (int) reader.ReadPackedUInt32();
                if ((num & 131072) != 0)
                    m_damageDodgedByEvades = (int) reader.ReadPackedUInt32();
                if ((num & 262144) != 0)
                    m_damageInterceptedByEvades = (int) reader.ReadPackedUInt32();
                if ((num & 524288) != 0)
                    m_myIncomingDamageReducedByCover = (int) reader.ReadPackedUInt32();
                if ((num & 1048576) != 0)
                    m_myOutgoingDamageReducedByCover = (int) reader.ReadPackedUInt32();
                if ((num & 2097152) != 0)
                    m_myIncomingOverkillDamageTaken = (int) reader.ReadPackedUInt32();
                if ((num & 4194304) != 0)
                    m_myOutgoingOverkillDamageDealt = (int) reader.ReadPackedUInt32();
                if ((num & 8388608) != 0)
                    m_myOutgoingExtraDamageFromEmpowered = (int) reader.ReadPackedUInt32();
                if ((num & 16777216) != 0)
                    m_myOutgoingDamageReducedFromWeakened = (int) reader.ReadPackedUInt32();
                if ((num & 33554432) != 0)
                    m_teamOutgoingDamageIncreasedByEmpoweredFromMe = (int) reader.ReadPackedUInt32();
                if ((num & 67108864) != 0)
                    m_teamIncomingDamageReducedByWeakenedFromMe = (int) reader.ReadPackedUInt32();
                if ((num & 134217728) != 0)
                    m_teamExtraEnergyGainFromMe = (int) reader.ReadPackedUInt32();
                if ((num & 268435456) != 0)
                    m_movementDeniedByMe = reader.ReadSingle();
                if ((num & 536870912) == 0)
                    return;
                m_totalEnemySighted = (int) reader.ReadPackedUInt32();
            }
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
        }

        public override string ToString()
        {
            return $"{nameof(ActorBehavior)}>(" +
                   ")";
        }
    }
}
