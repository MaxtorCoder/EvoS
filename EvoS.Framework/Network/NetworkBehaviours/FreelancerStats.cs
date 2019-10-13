using System;
using System.Collections.Generic;
using EvoS.Framework.Assets;
using EvoS.Framework.Assets.Serialized;
using EvoS.Framework.Assets.Serialized.Behaviours;
using EvoS.Framework.Game;
using EvoS.Framework.Logging;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.NetworkBehaviours
{
    [Serializable]
    [SerializedMonoBehaviour("FreelancerStats")]
    public class FreelancerStats : NetworkBehaviour
    {
        private static int kListm_values = 86738482;
        private SyncListInt m_values = new SyncListInt();
        private string m_freelancerTypeStr = "[unset]";

//        [Header("-- Whether to skip for localization for stat descriptions --")]
        public bool m_ignoreForLocalization;
        public List<string> m_name;
        public List<string> m_descriptions;

        public SyncListInt Values => m_values;
        public string FreelancerTypeStr => m_freelancerTypeStr;
        public bool IgnoreForLocalization => m_ignoreForLocalization;
        public List<string> Name => m_name;
        public List<string> Descriptions => m_descriptions;

        static FreelancerStats()
        {
            RegisterSyncListDelegate(typeof(FreelancerStats), kListm_values, InvokeSyncListm_values);
        }

        protected static void InvokeSyncListm_values(NetworkBehaviour obj, NetworkReader reader)
        {
            if (!EvoSGameConfig.NetworkIsClient)
                Log.Print(LogType.Error, "SyncList m_values called on server.");
            else
                ((FreelancerStats) obj).m_values.HandleMsg(reader);
        }

        public override void Awake()
        {
            m_values.InitializeBehaviour(this, kListm_values);
        }

        public override bool OnSerialize(NetworkWriter writer, bool forceAll)
        {
            if (forceAll)
            {
                SyncListInt.WriteInstance(writer, m_values);
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

                SyncListInt.WriteInstance(writer, m_values);
            }

            if (!flag)
                writer.WritePackedUInt32(syncVarDirtyBits);
            return flag;
        }

        public override void OnDeserialize(NetworkReader reader, bool initialState)
        {
            if (initialState)
            {
                SyncListInt.ReadReference(reader, m_values);
            }
            else
            {
                if (((int) reader.ReadPackedUInt32() & 1) == 0)
                    return;
                SyncListInt.ReadReference(reader, m_values);
            }
        }

        public override void DeserializeAsset(AssetFile assetFile, StreamReader stream)
        {
            m_ignoreForLocalization = stream.ReadBoolean();
            stream.AlignTo();
            m_name = new SerializedVector<string>(assetFile, stream);
            m_descriptions = new SerializedVector<string>(assetFile, stream);
        }

        public override string ToString()
        {
            return $"{nameof(FreelancerStats)}>(" +
                   $"{nameof(m_ignoreForLocalization)}: {m_ignoreForLocalization}, " +
                   $"{nameof(m_name)}: {m_name}, " +
                   $"{nameof(m_descriptions)}: {m_descriptions}, " +
                   ")";
        }

        public enum GeneralStats
        {
            STAT0,
            STAT1,
            STAT2,
            STAT3
        }

        public enum ArcherStats
        {
            ShieldArrowEffectiveShieldAndWeakenedMitigation,
            DashAndShootDamageDealtAndDodged,
            ArrowRainNumEnemiesRooted,
            HealArrowHealTotal
        }

        public enum BattleMonkStats
        {
            DamageReturnedByShield,
            AssistsWithRoot,
            DamageDealtPlusDodgedByCharge,
            ShieldsGrantedByUlt
        }

        public enum BazookaGirlStats
        {
            BigOneHits,
            DashesOutOfBigOne,
            UltKills,
            StickyBombCombos
        }

        public enum BlasterStats
        {
            DamageAddedFromOvercharge,
            LurkerDroneDamage,
            DamageDodgedByRoll,
            NumAssistsUsingUlt
        }

        public enum ClaymoreStats
        {
            DamageMitigatedByShout,
            DirectHitsWithCharge,
            AssistsWithDagger,
            UltDamage
        }

        public enum ClericStats
        {
            ReforgeHealFromMissingHealth,
            BoneShatterMovementDenied,
            SolarStrikeNumRevealApplied,
            UltEffectiveShielding
        }

        public enum DigitalSorceressStats
        {
            KnockbacksFromHeal,
            TimesPrimaryHitTwoOrMoreTargets,
            MitigationFromDebuffLaser,
            UltDamagePlusHealing
        }

        public enum DinoStats
        {
            MarkedAreaAttackDamage,
            DashOrShieldEffectiveShieldAndDamageEvaded,
            KnockbackDamageOnCastAndKnockback,
            ForceChaseNumChases
        }

        public enum ExoStats
        {
            TetherTrapTriggers,
            EffectiveShieldingFromSelfShield,
            UltDamage,
            MaxConsecutiveUltSweeps
        }

        public enum FireborgStats
        {
            IgniteDamage,
            GroundFireDamage,
            FireAuraDamage,
            BlastwaveDamage
        }

        public enum FishManStats
        {
            HealingFromHealCone,
            EelDamage,
            EnemiesHitByBubbleAoe,
            EnemiesKnockbackedByUlt
        }

        public enum GremlinsStats
        {
            MinesTriggeredByMovers,
            MinesTriggeredByKnockbacksFromMe,
            TurnsDirectlyHittingTwoEnemiesWithPrimary,
            DamageDoneByUlt
        }

        public enum IceborgStats
        {
            NumCoresTriggered,
            NumSlowsPlusRootsApplied,
            UltDamage,
            SelfShieldEffectiveShielding
        }

        public enum MantaStats
        {
            NumHitsThroughWalls,
            NumDamagingPutridSprayHits,
            HealingFromSelfHeal,
            NumEnemiesHitByUltCast
        }

        public enum MartyrStats
        {
            DamageRedirected,
            EnemiesDamagedByAoeOnHitEffect,
            TurnsWithMaxEnergy,
            UltDamagePlusHealing
        }

        public enum NanoSmithStats
        {
            EnergyFromChainLightning,
            VacuumBombHits,
            BarrierHits,
            PercentageOfTimesShieldedAllyWasDamaged
        }

        public enum NekoStats
        {
            NormalDiscNumDistinctEnemiesHitByReturn,
            EnlargeDiscExtraDamage,
            SeekerDiscDamage,
            FlipDashDamageDoneAndDodged,
            HomingDiscNumDistinctEnemiesHitByReturn
        }

        public enum RageBeastStats
        {
            EnergyGainedFromDamageTaken,
            HealingFromSelfHeal,
            KnockbackAssists,
            ChargeDamageDonePlusDodged
        }

        public enum RampartStats
        {
            HitsBlockedByShield,
            GrabAssists,
            MovementDebuffsAndKnockbacksPreventedByUnstoppable,
            UltDamageDealtPlusDodged
        }

        public enum RobotAnimalStats
        {
            HealingFromPrimary,
            UltHits,
            DragAssists,
            DamageDonePlusDodgedByPounce
        }

        public enum SamuraiStats
        {
            EffectiveShielding_RensFury,
            NumEnemiesHit_WindBlade,
            DamageDealtAndDodged_RushingSteel,
            DamageDealtAndDodged_Ult
        }

        public enum ScampStats
        {
            DashDamageDoneAndAvoided,
            TetherNumKnockbacks,
            DelayedAoeDamageDone,
            UltShieldGenerated
        }

        public enum ScoundrelStats
        {
            TargetsHitByConeAoe,
            DashersHitByTrapwire,
            DamageDodgedByEvasionRoll,
            DamageDodgedPlusDealtByUlt
        }

        public enum SenseiStats
        {
            NumAlliesHitByHeal,
            NumBuffsPlusDebuffsFromAppendStatus,
            DamageDodgedPlusDamageDealtPlusHealingDealtByDash,
            DamageDoneByUlt
        }

        public enum SniperStats
        {
            DecisionPhasesNotVisibleToEnemies,
            DecisionPhasesWithNoNearbyEnemies,
            EnergyGainedByVortexRound,
            DamageDoneByUlt
        }

        public enum SoldierStats
        {
            DamageDealtPlusDodgedByDash,
            DamageFromGrenadesThrownOverWalls,
            DamageAddedByStimMight,
            TargetsHitByUlt
        }

        public enum SpaceMarineStats
        {
            MissilesHit,
            NumSlowsPlusRootsApplied,
            HealingDealtByUlt,
            DamageDealtByUlt
        }

        public enum SparkStats
        {
            TurnsTetheredToAllies,
            TurnsTetheredToEnemies,
            DamageDodgedPlusDamageDealtPlusHealingDealtByDash,
            HealingFromUlt
        }

        public enum TeleportingNinjaStats
        {
            DamageDodgedWithDash,
            NumDetonationsOfMark,
            TurnsNotVisibleToEnemies,
            DamageDonePlusDodgedWithUlt
        }

        public enum ThiefStats
        {
            PowerUpsStolen,
            ProximityMineHits,
            TimesSmokeBombHitsHidAllies,
            UltDamage
        }

        public enum TrackerStats
        {
            DamageDoneByDrone,
            NumTrackingProjectileHits,
            DamageMitigatedByTranqDart,
            DamageDodgedWithEvade
        }

        public enum TricksterStats
        {
            DamageDodgedWithSwap,
            TargetsHitByThreeImages,
            TargetsHitByPhotonSpray,
            TargetsHitByZapTrap
        }

        public enum ValkyrieStats
        {
            DamageMitigatedByCoverOnTurnsWithGuard,
            DamageDoneByThrowShieldAndKnockback,
            DamageMitigatedFromWeakenedAndDodgedDashAoe,
            NumKnockbackTargetsWithUlt
        }
    }
}
