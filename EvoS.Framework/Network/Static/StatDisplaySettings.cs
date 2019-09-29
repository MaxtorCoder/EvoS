using System.Linq;

namespace EvoS.Framework.Network.Static
{
    public static class StatDisplaySettings
    {
//        public static string GetLocalizedName(StatType TypeOfStat)
//        {
//            return StringUtil.TR_StatName(TypeOfStat);
//        }
//
//        public static string GetLocalizedDescription(StatType TypeOfStat)
//        {
//            return StringUtil.TR_StatDescription(TypeOfStat);
//        }

        public static bool IsStatADisplayedStat(StatType TypeOfStat)
        {
            return GeneralStats.Contains(TypeOfStat) ||
                   FirepowerStats.Contains(TypeOfStat) ||
                   SupportStats.Contains(TypeOfStat) ||
                   FrontlinerStats.Contains(TypeOfStat);
        }

        public static StatType[] GeneralStats = {
            StatType.TotalAssists,
            StatType.TotalDeaths,
            StatType.TotalBadgePoints,
            StatType.EnergyGainPerTurn
        };

        public static StatType[] FirepowerStats = {
            StatType.DamagePerTurn,
            StatType.NetBoostedOutgoingDamage,
            StatType.DamageEfficiency,
            StatType.KillParticipation
        };

        public static StatType[] SupportStats = {
            StatType.EffectiveHealAndAbsorb,
            StatType.TeamDamageAdjustedByMe,
            StatType.TeamExtraEnergyByEnergizedFromMe,
            StatType.IncomingDamageReducedByCover
        };

        public static StatType[] FrontlinerStats = {
            StatType.DamageTakenPerLife,
            StatType.IncomingDamageDodgeByEvade,
            StatType.EnemiesSightedPerLife,
            StatType.MovementDenied
        };

        public interface IPersistatedStatValueSupplier
        {
            float? GetStat(StatType Type);

            float? GetFreelancerStat(int FreelancerStatIndex);
        }

        [EvosMessage(35)]
        public enum StatType
        {
            TotalAssists,
            TotalDeaths,
            TotalBadgePoints,
            EnergyGainPerTurn,
            DamagePerTurn,
            NetBoostedOutgoingDamage,
            DamageEfficiency,
            KillParticipation,
            EffectiveHealAndAbsorb,
            TeamDamageAdjustedByMe,
            TeamExtraEnergyByEnergizedFromMe,
            MovementDenied,
            DamageTakenPerLife,
            IncomingDamageDodgeByEvade,
            IncomingDamageReducedByCover,
            EnemiesSightedPerLife,
            TotalTurns,
            TotalTeamDamageReceived,
            TankingPerLife,
            TeamMitigation,
            SupportPerTurn,
            DamageDonePerLife,
            DamageTakenPerTurn,
            MMR,
            AvgLifeSpan,
            SecondsPlayed,
            MatchesWon
        }
    }
}
