using System;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Logging;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(46)]
    public class MatchFreelancerStats : ICloneable, StatDisplaySettings.IPersistatedStatValueSupplier
    {
        public PersistedStatBucket PersistedStatBucket { get; set; }

        public CharacterType CharacterType { get; set; }

        public int ActiveSeason { get; set; }

        public float TotalAssists { get; set; }

        public float TotalDeaths { get; set; }

        public float? TotalBadgePoints { get; set; }

        public float EnergyGainPerTurn { get; set; }

        public float? DamagePerTurn { get; set; }

        public float NetBoostedOutgoingDamage { get; set; }

        public float DamageEfficiency { get; set; }

        public float KillParticipation { get; set; }

        public float TeamDamageAdjustedByMe { get; set; }

        public float TeamExtraEnergyByEnergizedFromMe { get; set; }

        public float MovementDenied { get; set; }

        public float? DamageTakenPerLife { get; set; }

        public float IncomingDamageDodgeByEvade { get; set; }

        public float IncomingDamageReducedByCover { get; set; }

        public float EnemiesSightedPerTurn { get; set; }

        public float TotalTurns { get; set; }

        public float TotalTeamDamageReceived { get; set; }

        public float? TankingPerLife { get; set; }

        public float? TeamMitigation { get; set; }

        public float? SupportPerTurn { get; set; }

        public float? DamageDonePerLife { get; set; }

        public float? DamageTakenPerTurn { get; set; }

        public float? MMR { get; set; }

        public int? Freelancer0 { get; set; }

        public int? Freelancer1 { get; set; }

        public int? Freelancer2 { get; set; }

        public int? Freelancer3 { get; set; }

        public float? GetStat(StatDisplaySettings.StatType Type)
        {
            switch (Type)
            {
                case StatDisplaySettings.StatType.TotalAssists:
                    return TotalAssists;
                case StatDisplaySettings.StatType.TotalDeaths:
                    return TotalDeaths;
                case StatDisplaySettings.StatType.TotalBadgePoints:
                    return TotalBadgePoints;
                case StatDisplaySettings.StatType.EnergyGainPerTurn:
                    return EnergyGainPerTurn;
                case StatDisplaySettings.StatType.DamagePerTurn:
                    return DamagePerTurn;
                case StatDisplaySettings.StatType.NetBoostedOutgoingDamage:
                    if (TotalTurns > 0f)
                    {
                        return NetBoostedOutgoingDamage / TotalTurns;
                    }

                    return null;
                case StatDisplaySettings.StatType.DamageEfficiency:
                    return DamageEfficiency;
                case StatDisplaySettings.StatType.KillParticipation:
                    return KillParticipation;
                case StatDisplaySettings.StatType.EffectiveHealAndAbsorb:
                    return SupportPerTurn;
                case StatDisplaySettings.StatType.TeamDamageAdjustedByMe:
                    if (TotalTurns > 0f)
                    {
                        return TeamDamageAdjustedByMe / TotalTurns;
                    }

                    return null;
                case StatDisplaySettings.StatType.TeamExtraEnergyByEnergizedFromMe:
                    if (TotalTurns > 0f)
                    {
                        return TeamExtraEnergyByEnergizedFromMe / TotalTurns;
                    }

                    return null;
                case StatDisplaySettings.StatType.MovementDenied:
                    if (TotalTurns > 0f)
                    {
                        return MovementDenied / TotalTurns;
                    }

                    return null;
                case StatDisplaySettings.StatType.DamageTakenPerLife:
                    return DamageTakenPerLife;
                case StatDisplaySettings.StatType.IncomingDamageDodgeByEvade:
                    return IncomingDamageDodgeByEvade / (TotalDeaths + 1f);
                case StatDisplaySettings.StatType.IncomingDamageReducedByCover:
                    return IncomingDamageReducedByCover / (TotalDeaths + 1f);
                case StatDisplaySettings.StatType.EnemiesSightedPerLife:
                    return EnemiesSightedPerTurn;
                case StatDisplaySettings.StatType.TotalTurns:
                    return TotalTurns;
                case StatDisplaySettings.StatType.TotalTeamDamageReceived:
                    return TotalTeamDamageReceived;
                case StatDisplaySettings.StatType.TankingPerLife:
                    return TankingPerLife;
                case StatDisplaySettings.StatType.TeamMitigation:
                    return TeamMitigation;
                case StatDisplaySettings.StatType.SupportPerTurn:
                    return SupportPerTurn;
                case StatDisplaySettings.StatType.DamageDonePerLife:
                    return DamageDonePerLife;
                case StatDisplaySettings.StatType.DamageTakenPerTurn:
                    return DamageTakenPerTurn;
                case StatDisplaySettings.StatType.MMR:
                    if (MMR == null)
                    {
                        return null;
                    }

                    if (MMR.Value <= 0f || MMR.Value == 1500f)
                    {
                        return null;
                    }

                    return MMR;
                case StatDisplaySettings.StatType.AvgLifeSpan:
                    return TotalTurns / (TotalDeaths + 1f);
                default:
                    return null;
            }
        }

        public float? GetFreelancerStat(int FreelancerStatIndex)
        {
            switch (FreelancerStatIndex)
            {
                case 0:
                {
                    return Freelancer0;
                }
                case 1:
                {
                    return Freelancer1;
                }
                case 2:
                {
                    return Freelancer2;
                }
                case 3:
                {
                    return Freelancer3;
                }
                default:
                    Log.Print(LogType.Error, $"Unknown freelancer stat index: {FreelancerStatIndex}");
                    return null;
            }
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}
