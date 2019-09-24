using System;
using System.Collections.Generic;
using EvoS.Framework.Logging;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(40)]
    public class PersistedStats : ICloneable
    {
        public PersistedStats()
        {
            TotalDeaths = new PersistedStatEntry();
            TotalPlayerKills = new PersistedStatEntry();
            TotalPlayerAssists = new PersistedStatEntry();
            TotalPlayerDamage = new PersistedStatEntry();
            TotalPlayerHealing = new PersistedStatEntry();
            TotalPlayerAbsorb = new PersistedStatEntry();
            TotalPlayerDamageReceived = new PersistedStatEntry();
            TotalBadgePoints = new PersistedStatFloatEntry();
            NetDamageAvoidedByEvades = new PersistedStatEntry();
            NetDamageAvoidedByEvadesPerLife = new PersistedStatFloatEntry();
            DamageDodgedByEvades = new PersistedStatEntry();
            DamageInterceptedByEvades = new PersistedStatEntry();
            MyIncomingDamageReducedByCover = new PersistedStatEntry();
            MyIncomingDamageReducedByCoverPerLife = new PersistedStatFloatEntry();
            MyOutgoingDamageReducedByCover = new PersistedStatEntry();
            MyOutgoingExtraDamageFromEmpowered = new PersistedStatEntry();
            MyOutgoingDamageReducedFromWeakened = new PersistedStatEntry();
            TeamOutgoingDamageIncreasedByEmpoweredFromMe = new PersistedStatEntry();
            TeamIncomingDamageReducedByWeakenedFromMe = new PersistedStatEntry();
            MovementDeniedByMePerTurn = new PersistedStatFloatEntry();
            EnergyGainPerTurn = new PersistedStatFloatEntry();
            DamagePerTurn = new PersistedStatFloatEntry();
            BoostedOutgoingDamagePerTurn = new PersistedStatFloatEntry();
            DamageEfficiency = new PersistedStatFloatEntry();
            KillParticipation = new PersistedStatFloatEntry();
            EffectiveHealing = new PersistedStatEntry();
            TeamDamageAdjustedByMe = new PersistedStatEntry();
            TeamDamageSwingByMePerTurn = new PersistedStatFloatEntry();
            TeamExtraEnergyByEnergizedFromMe = new PersistedStatEntry();
            TeamBoostedEnergyByMePerTurn = new PersistedStatFloatEntry();
            TeamDamageReceived = new PersistedStatEntry();
            DamageTakenPerLife = new PersistedStatFloatEntry();
            EnemiesSightedPerTurn = new PersistedStatFloatEntry();
            TotalTurns = new PersistedStatFloatEntry();
            TankingPerLife = new PersistedStatFloatEntry();
            TeamMitigation = new PersistedStatFloatEntry();
            SupportPerTurn = new PersistedStatFloatEntry();
            DamageDonePerLife = new PersistedStatFloatEntry();
            DamageTakenPerTurn = new PersistedStatFloatEntry();
            AvgLifeSpan = new PersistedStatFloatEntry();
            SecondsPlayed = new PersistedStatFloatEntry();
            MatchesWon = new PersistedStatEntry();
            FreelancerSpecificStats = new List<PersistedStatEntry>();
        }

        public PersistedStatEntry TotalDeaths { get; set; }

        public PersistedStatEntry TotalPlayerKills { get; set; }

        public PersistedStatEntry TotalPlayerAssists { get; set; }

        public PersistedStatEntry TotalPlayerDamage { get; set; }

        public PersistedStatEntry TotalPlayerHealing { get; set; }

        public PersistedStatEntry TotalPlayerAbsorb { get; set; }

        public PersistedStatEntry TotalPlayerDamageReceived { get; set; }

        public PersistedStatFloatEntry TotalBadgePoints { get; set; }

        public PersistedStatEntry NetDamageAvoidedByEvades { get; set; }

        public PersistedStatFloatEntry NetDamageAvoidedByEvadesPerLife { get; set; }

        public PersistedStatEntry DamageDodgedByEvades { get; set; }

        public PersistedStatEntry DamageInterceptedByEvades { get; set; }

        public PersistedStatEntry MyIncomingDamageReducedByCover { get; set; }

        public PersistedStatFloatEntry MyIncomingDamageReducedByCoverPerLife { get; set; }

        public PersistedStatEntry MyOutgoingDamageReducedByCover { get; set; }

        public PersistedStatEntry MyOutgoingExtraDamageFromEmpowered { get; set; }

        public PersistedStatEntry MyOutgoingDamageReducedFromWeakened { get; set; }

        public PersistedStatEntry TeamOutgoingDamageIncreasedByEmpoweredFromMe { get; set; }

        public PersistedStatEntry TeamIncomingDamageReducedByWeakenedFromMe { get; set; }

        public PersistedStatFloatEntry MovementDeniedByMePerTurn { get; set; }

        public PersistedStatFloatEntry EnergyGainPerTurn { get; set; }

        public PersistedStatFloatEntry DamagePerTurn { get; set; }

        public PersistedStatFloatEntry BoostedOutgoingDamagePerTurn { get; set; }

        public PersistedStatFloatEntry DamageEfficiency { get; set; }

        public PersistedStatFloatEntry KillParticipation { get; set; }

        public PersistedStatEntry EffectiveHealing { get; set; }

        public PersistedStatEntry TeamDamageAdjustedByMe { get; set; }

        public PersistedStatFloatEntry TeamDamageSwingByMePerTurn { get; set; }

        public PersistedStatEntry TeamExtraEnergyByEnergizedFromMe { get; set; }

        public PersistedStatFloatEntry TeamBoostedEnergyByMePerTurn { get; set; }

        public PersistedStatEntry TeamDamageReceived { get; set; }

        public PersistedStatFloatEntry DamageTakenPerLife { get; set; }

        public PersistedStatFloatEntry EnemiesSightedPerTurn { get; set; }

        public PersistedStatFloatEntry TotalTurns { get; set; }

        public PersistedStatFloatEntry TankingPerLife { get; set; }

        public PersistedStatFloatEntry TeamMitigation { get; set; }

        public PersistedStatFloatEntry SupportPerTurn { get; set; }

        public PersistedStatFloatEntry DamageDonePerLife { get; set; }

        public PersistedStatFloatEntry DamageTakenPerTurn { get; set; }

        public PersistedStatFloatEntry AvgLifeSpan { get; set; }

        public PersistedStatFloatEntry SecondsPlayed { get; set; }

        public PersistedStatEntry MatchesWon { get; set; }

        [EvosMessage(43)]
        public List<PersistedStatEntry> FreelancerSpecificStats { get; set; }

        public IPersistedGameplayStat GetGameplayStat(StatDisplaySettings.StatType TypeOfStat)
        {
            switch (TypeOfStat)
            {
                case StatDisplaySettings.StatType.IncomingDamageDodgeByEvade:
                    return NetDamageAvoidedByEvadesPerLife;
                case StatDisplaySettings.StatType.TotalBadgePoints:
                    return TotalBadgePoints;
                case StatDisplaySettings.StatType.IncomingDamageReducedByCover:
                    return MyIncomingDamageReducedByCoverPerLife;
                case StatDisplaySettings.StatType.TotalAssists:
                    return TotalPlayerAssists;
                case StatDisplaySettings.StatType.TotalDeaths:
                    return TotalDeaths;
                case StatDisplaySettings.StatType.MovementDenied:
                    return MovementDeniedByMePerTurn;
                case StatDisplaySettings.StatType.EnergyGainPerTurn:
                    return EnergyGainPerTurn;
                case StatDisplaySettings.StatType.DamagePerTurn:
                    return DamagePerTurn;
                case StatDisplaySettings.StatType.NetBoostedOutgoingDamage:
                    return BoostedOutgoingDamagePerTurn;
                case StatDisplaySettings.StatType.DamageEfficiency:
                    return DamageEfficiency;
                case StatDisplaySettings.StatType.KillParticipation:
                    return KillParticipation;
                case StatDisplaySettings.StatType.EffectiveHealAndAbsorb:
                    return SupportPerTurn;
                case StatDisplaySettings.StatType.TeamDamageAdjustedByMe:
                    return TeamDamageSwingByMePerTurn;
                case StatDisplaySettings.StatType.TeamExtraEnergyByEnergizedFromMe:
                    return TeamBoostedEnergyByMePerTurn;
                case StatDisplaySettings.StatType.DamageTakenPerLife:
                    return DamageTakenPerLife;
                case StatDisplaySettings.StatType.EnemiesSightedPerLife:
                    return EnemiesSightedPerTurn;
                case StatDisplaySettings.StatType.TankingPerLife:
                    return TankingPerLife;
                case StatDisplaySettings.StatType.DamageDonePerLife:
                    return DamageDonePerLife;
                case StatDisplaySettings.StatType.TeamMitigation:
                    return TeamMitigation;
                case StatDisplaySettings.StatType.TotalTurns:
                    return TotalTurns;
                case StatDisplaySettings.StatType.TotalTeamDamageReceived:
                    return TeamDamageReceived;
                case StatDisplaySettings.StatType.SupportPerTurn:
                    return SupportPerTurn;
                case StatDisplaySettings.StatType.DamageTakenPerTurn:
                    return DamageTakenPerTurn;
                case StatDisplaySettings.StatType.SecondsPlayed:
                    return SecondsPlayed;
                case StatDisplaySettings.StatType.MatchesWon:
                    return MatchesWon;
                default:
                    Log.Print(LogType.Warning, "Attempting to display a stat that isn't categorized: " + TypeOfStat);
                    return null;
            }
        }

        public void CombineStats(PersistedStats StatsToBeMerged)
        {
            TotalDeaths.CombineStats(StatsToBeMerged.TotalDeaths);
            TotalPlayerKills.CombineStats(StatsToBeMerged.TotalPlayerKills);
            TotalPlayerAssists.CombineStats(StatsToBeMerged.TotalPlayerAssists);
            TotalPlayerDamage.CombineStats(StatsToBeMerged.TotalPlayerDamage);
            TotalPlayerHealing.CombineStats(StatsToBeMerged.TotalPlayerHealing);
            TotalPlayerAbsorb.CombineStats(StatsToBeMerged.TotalPlayerAbsorb);
            TotalPlayerDamageReceived.CombineStats(StatsToBeMerged.TotalPlayerDamageReceived);
            TotalBadgePoints.CombineStats(StatsToBeMerged.TotalBadgePoints);
            NetDamageAvoidedByEvades.CombineStats(StatsToBeMerged.NetDamageAvoidedByEvades);
            NetDamageAvoidedByEvadesPerLife.CombineStats(StatsToBeMerged.NetDamageAvoidedByEvadesPerLife);
            DamageDodgedByEvades.CombineStats(StatsToBeMerged.DamageDodgedByEvades);
            DamageInterceptedByEvades.CombineStats(StatsToBeMerged.DamageInterceptedByEvades);
            MyIncomingDamageReducedByCover.CombineStats(StatsToBeMerged.MyIncomingDamageReducedByCover);
            MyIncomingDamageReducedByCoverPerLife.CombineStats(StatsToBeMerged
                .MyIncomingDamageReducedByCoverPerLife);
            MyOutgoingDamageReducedByCover.CombineStats(StatsToBeMerged.MyOutgoingDamageReducedByCover);
            MyOutgoingExtraDamageFromEmpowered.CombineStats(StatsToBeMerged.MyOutgoingExtraDamageFromEmpowered);
            MyOutgoingDamageReducedFromWeakened.CombineStats(StatsToBeMerged.MyOutgoingDamageReducedFromWeakened);
            TeamOutgoingDamageIncreasedByEmpoweredFromMe.CombineStats(StatsToBeMerged
                .TeamOutgoingDamageIncreasedByEmpoweredFromMe);
            TeamIncomingDamageReducedByWeakenedFromMe.CombineStats(StatsToBeMerged
                .TeamIncomingDamageReducedByWeakenedFromMe);
            MovementDeniedByMePerTurn.CombineStats(StatsToBeMerged.MovementDeniedByMePerTurn);
            EnergyGainPerTurn.CombineStats(StatsToBeMerged.EnergyGainPerTurn);
            DamagePerTurn.CombineStats(StatsToBeMerged.DamagePerTurn);
            BoostedOutgoingDamagePerTurn.CombineStats(StatsToBeMerged.BoostedOutgoingDamagePerTurn);
            DamageEfficiency.CombineStats(StatsToBeMerged.DamageEfficiency);
            KillParticipation.CombineStats(StatsToBeMerged.KillParticipation);
            EffectiveHealing.CombineStats(StatsToBeMerged.EffectiveHealing);
            TeamDamageAdjustedByMe.CombineStats(StatsToBeMerged.TeamDamageAdjustedByMe);
            TeamDamageSwingByMePerTurn.CombineStats(StatsToBeMerged.TeamDamageSwingByMePerTurn);
            TeamExtraEnergyByEnergizedFromMe.CombineStats(StatsToBeMerged.TeamExtraEnergyByEnergizedFromMe);
            TeamBoostedEnergyByMePerTurn.CombineStats(StatsToBeMerged.TeamBoostedEnergyByMePerTurn);
            TeamDamageReceived.CombineStats(StatsToBeMerged.TeamDamageReceived);
            DamageTakenPerLife.CombineStats(StatsToBeMerged.DamageTakenPerLife);
            EnemiesSightedPerTurn.CombineStats(StatsToBeMerged.EnemiesSightedPerTurn);
            TotalTurns.CombineStats(StatsToBeMerged.TotalTurns);
            TankingPerLife.CombineStats(StatsToBeMerged.TankingPerLife);
            TeamMitigation.CombineStats(StatsToBeMerged.TeamMitigation);
            SupportPerTurn.CombineStats(StatsToBeMerged.SupportPerTurn);
            DamageDonePerLife.CombineStats(StatsToBeMerged.DamageDonePerLife);
            DamageTakenPerTurn.CombineStats(StatsToBeMerged.DamageTakenPerTurn);
            SecondsPlayed.CombineStats(StatsToBeMerged.SecondsPlayed);
            MatchesWon.CombineStats(StatsToBeMerged.MatchesWon);
            for (int i = 0; i < FreelancerSpecificStats.Count; i++)
            {
                if (i < StatsToBeMerged.FreelancerSpecificStats.Count)
                {
                    FreelancerSpecificStats[i].CombineStats(StatsToBeMerged.FreelancerSpecificStats[i]);
                }
            }
        }

        public PersistedStatEntry GetFreelancerStat(int index)
        {
            if (FreelancerSpecificStats != null && -1 < index && index < FreelancerSpecificStats.Count)
            {
                return FreelancerSpecificStats[index];
            }

            return new PersistedStatEntry();
        }

        public object Clone()
        {
            PersistedStats persistedStats = new PersistedStats();
            persistedStats.TotalDeaths = TotalDeaths.GetCopy();
            persistedStats.TotalPlayerKills = TotalPlayerKills.GetCopy();
            persistedStats.TotalPlayerAssists = TotalPlayerAssists.GetCopy();
            persistedStats.TotalPlayerDamage = TotalPlayerDamage.GetCopy();
            persistedStats.TotalPlayerHealing = TotalPlayerHealing.GetCopy();
            persistedStats.TotalPlayerAbsorb = TotalPlayerAbsorb.GetCopy();
            persistedStats.TotalPlayerDamageReceived = TotalPlayerDamageReceived.GetCopy();
            persistedStats.TotalBadgePoints = TotalBadgePoints.GetCopy();
            persistedStats.NetDamageAvoidedByEvades = NetDamageAvoidedByEvades.GetCopy();
            persistedStats.NetDamageAvoidedByEvadesPerLife = NetDamageAvoidedByEvadesPerLife.GetCopy();
            persistedStats.DamageDodgedByEvades = DamageDodgedByEvades.GetCopy();
            persistedStats.DamageInterceptedByEvades = DamageInterceptedByEvades.GetCopy();
            persistedStats.MyIncomingDamageReducedByCover = MyIncomingDamageReducedByCover.GetCopy();
            persistedStats.MyIncomingDamageReducedByCoverPerLife = MyIncomingDamageReducedByCoverPerLife.GetCopy();
            persistedStats.MyOutgoingDamageReducedByCover = MyOutgoingDamageReducedByCover.GetCopy();
            persistedStats.MyOutgoingDamageReducedFromWeakened = MyOutgoingDamageReducedFromWeakened.GetCopy();
            persistedStats.MyOutgoingExtraDamageFromEmpowered = MyOutgoingExtraDamageFromEmpowered.GetCopy();
            persistedStats.TeamIncomingDamageReducedByWeakenedFromMe =
                TeamIncomingDamageReducedByWeakenedFromMe.GetCopy();
            persistedStats.TeamOutgoingDamageIncreasedByEmpoweredFromMe =
                TeamOutgoingDamageIncreasedByEmpoweredFromMe.GetCopy();
            persistedStats.MovementDeniedByMePerTurn = MovementDeniedByMePerTurn.GetCopy();
            persistedStats.EnergyGainPerTurn = EnergyGainPerTurn.GetCopy();
            persistedStats.DamagePerTurn = DamagePerTurn.GetCopy();
            persistedStats.BoostedOutgoingDamagePerTurn = BoostedOutgoingDamagePerTurn.GetCopy();
            persistedStats.DamageEfficiency = DamageEfficiency.GetCopy();
            persistedStats.KillParticipation = KillParticipation.GetCopy();
            persistedStats.EffectiveHealing = EffectiveHealing.GetCopy();
            persistedStats.TeamDamageAdjustedByMe = TeamDamageAdjustedByMe.GetCopy();
            persistedStats.TeamDamageSwingByMePerTurn = TeamDamageSwingByMePerTurn.GetCopy();
            persistedStats.TeamExtraEnergyByEnergizedFromMe = TeamExtraEnergyByEnergizedFromMe.GetCopy();
            persistedStats.TeamBoostedEnergyByMePerTurn = TeamBoostedEnergyByMePerTurn.GetCopy();
            persistedStats.TeamDamageReceived = TeamDamageReceived.GetCopy();
            persistedStats.DamageTakenPerLife = DamageTakenPerLife.GetCopy();
            persistedStats.EnemiesSightedPerTurn = EnemiesSightedPerTurn.GetCopy();
            persistedStats.TotalTurns = TotalTurns.GetCopy();
            persistedStats.TankingPerLife = TankingPerLife.GetCopy();
            persistedStats.TeamMitigation = TeamMitigation.GetCopy();
            persistedStats.SupportPerTurn = SupportPerTurn.GetCopy();
            persistedStats.DamageDonePerLife = DamageDonePerLife.GetCopy();
            persistedStats.DamageTakenPerTurn = DamageTakenPerTurn.GetCopy();
            persistedStats.SecondsPlayed = SecondsPlayed.GetCopy();
            persistedStats.MatchesWon = MatchesWon.GetCopy();
            if (FreelancerSpecificStats == null)
            {
                persistedStats.FreelancerSpecificStats = null;
            }
            else
            {
                persistedStats.FreelancerSpecificStats = new List<PersistedStatEntry>();
                for (int i = 0; i < FreelancerSpecificStats.Count; i++)
                {
                    PersistedStatEntry item = new PersistedStatEntry();
                    item = (PersistedStatEntry) FreelancerSpecificStats[i].Clone();
                    persistedStats.FreelancerSpecificStats.Add(item);
                }
            }

            return persistedStats;
        }
    }
}
