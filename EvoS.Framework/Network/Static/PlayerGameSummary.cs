using System;
using System.Collections.Generic;
using System.Linq;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    public class PlayerGameSummary : StatDisplaySettings.IPersistatedStatValueSupplier
    {
        public bool IsInTeamA()
        {
            return Team == 0;
        }

        public bool IsInTeamB()
        {
            return Team == 1;
        }

        public bool IsSpectator()
        {
            return Team == 3;
        }

        public GameResult ResultForWin()
        {
            return (!IsInTeamA()) ? GameResult.TeamBWon : GameResult.TeamAWon;
        }

        public int GetTotalGainedXPAccount()
        {
            return BaseXPGainedAccount + GGXPGainedAccount + PlayWithFriendXPGainedAccount +
                   QuestXPGainedAccount + FirstWinXpGainedAccount + QueueTimeXpGainedAccount +
                   FreelancerOwnedXPAmount;
        }

        public int GetTotalGainedXPCharacter()
        {
            return BaseXPGainedCharacter + GGXPGainedCharacter + ConsumableXPGainedCharacter +
                   PlayWithFriendXPGainedCharacter + QuestXPGainedCharacter + FirstWinXpGainedCharacter +
                   QueueTimeXpGainedCharacter + FreelancerOwnedXPAmount;
        }

        public int GetTotalGainedISO()
        {
            return BaseISOGained + GGISOGained;
        }

        public int GetTotalGainedFreelancerCurrency()
        {
            return BaseFreelancerCurrencyGained + WinFreelancerCurrencyGained +
                   GGFreelancerCurrencyGained + EventBonusFreelancerCurrencyGained;
        }

        public int GetContribution()
        {
            return TotalPlayerDamage + TotalPlayerAbsorb + GetTotalHealingFromAbility();
        }

        public int GetTotalHealingFromAbility()
        {
            return (from a in AbilityGameSummaryList
                select a.TotalHealing).Sum();
        }

        public float? GetNumLives()
        {
            if (TotalGameTurns > 0)
            {
                return Math.Max(1f, NumDeaths + 1);
            }

            return null;
        }

        public float? GetDamageDealtPerTurn()
        {
            if (TotalGameTurns > 0)
            {
                return TotalPlayerDamage / (float) TotalGameTurns;
            }

            return null;
        }

        public float? GetTeamEnergyBoostedByMePerTurn()
        {
            if (TotalGameTurns > 0)
            {
                return TeamExtraEnergyByEnergizedFromMe / (float) TotalGameTurns;
            }

            return null;
        }

        public float? GetTeamDamageSwingByMePerTurn()
        {
            if (TotalGameTurns > 0)
            {
                return GetTotalTeamDamageAdjustedByMe() / (float) TotalGameTurns;
            }

            return null;
        }

        public float? GetDamageTakenPerLife()
        {
            float? numLives = GetNumLives();
            if (numLives != null)
            {
                return TotalPlayerDamageReceived / numLives.Value;
            }

            return null;
        }

        public float GetBoostedDamagePerTurn()
        {
            if (TotalGameTurns > 0)
            {
                return MyOutgoingExtraDamageFromEmpowered / (float) TotalGameTurns;
            }

            return 0f;
        }

        public int GetTotalTeamDamageAdjustedByMe()
        {
            return TeamOutgoingDamageIncreasedByEmpoweredFromMe + TeamIncomingDamageReducedByWeakenedFromMe;
        }

        public string ToPlayerInfoString()
        {
            return string.Format(
                "- - - - - - - - - -\n[User Name] {0}\n[Account Id] {1}\n[Character Played] {2}\n[Skin] {3}\n[Team] {4}\n", InGameName, AccountId, CharacterPlayed, CharacterSkinIndex, Team);
        }

        public float? GetTankingPerLife()
        {
            float? numLives = GetNumLives();
            if (numLives != null)
            {
                return (TotalPlayerDamageReceived + NetDamageAvoidedByEvades +
                        MyIncomingDamageReducedByCover) / numLives.Value;
            }

            return null;
        }

        public float? GetTeamMitigation()
        {
            float num = TeamIncomingDamageReducedByWeakenedFromMe + TotalTeamDamageReceived;
            if (num == 0f)
            {
                return null;
            }

            float num2 = EffectiveHealing + TotalPlayerAbsorb +
                         TeamIncomingDamageReducedByWeakenedFromMe;
            return num2 / num;
        }

        public float? GetSupportPerTurn()
        {
            float num = EffectiveHealing + TotalPlayerAbsorb;
            float num2 = TotalGameTurns;
            if (num2 == 0f)
            {
                return null;
            }

            return num / num2;
        }

        public float? GetDamageDonePerLife()
        {
            float? numLives = GetNumLives();
            if (numLives != null)
            {
                return TotalPlayerDamage / numLives.Value;
            }

            return null;
        }

        public float? GetNetDamageDodgedPerLife()
        {
            float? numLives = GetNumLives();
            if (numLives != null)
            {
                return NetDamageAvoidedByEvades / numLives.Value;
            }

            return null;
        }

        public float? GetIncomingDamageMitigatedByCoverPerLife()
        {
            float? numLives = GetNumLives();
            if (numLives != null)
            {
                return MyIncomingDamageReducedByCover / numLives.Value;
            }

            return null;
        }

        public float? GetAvgLifeSpan()
        {
            float? numLives = GetNumLives();
            if (numLives != null)
            {
                return Math.Max(1, TotalGameTurns) / numLives.Value;
            }

            return null;
        }

        public float? GetDamageTakenPerTurn()
        {
            if (TotalGameTurns > 0)
            {
                return TotalPlayerDamageReceived / (float) TotalGameTurns;
            }

            return null;
        }

        public float GetMovementDeniedPerTurn()
        {
            if (TotalGameTurns > 0)
            {
                return MovementDeniedByMe / TotalGameTurns;
            }

            return 0f;
        }

        public float? GetStat(StatDisplaySettings.StatType TypeOfStat)
        {
            if (TypeOfStat == StatDisplaySettings.StatType.IncomingDamageDodgeByEvade)
            {
                return GetNetDamageDodgedPerLife();
            }

            if (TypeOfStat == StatDisplaySettings.StatType.IncomingDamageReducedByCover)
            {
                return GetIncomingDamageMitigatedByCoverPerLife();
            }

            if (TypeOfStat == StatDisplaySettings.StatType.TotalAssists)
            {
                return NumAssists;
            }

            if (TypeOfStat == StatDisplaySettings.StatType.TotalDeaths)
            {
                return NumDeaths;
            }

            if (TypeOfStat == StatDisplaySettings.StatType.TotalBadgePoints)
            {
                return TotalBadgePoints;
            }

            if (TypeOfStat == StatDisplaySettings.StatType.MovementDenied)
            {
                return GetMovementDeniedPerTurn();
            }

            if (TypeOfStat == StatDisplaySettings.StatType.EnergyGainPerTurn)
            {
                return EnergyGainPerTurn;
            }

            if (TypeOfStat == StatDisplaySettings.StatType.DamagePerTurn)
            {
                return GetDamageDealtPerTurn();
            }

            if (TypeOfStat == StatDisplaySettings.StatType.NetBoostedOutgoingDamage)
            {
                return GetBoostedDamagePerTurn();
            }

            if (TypeOfStat == StatDisplaySettings.StatType.DamageEfficiency)
            {
                return DamageEfficiency;
            }

            if (TypeOfStat == StatDisplaySettings.StatType.KillParticipation)
            {
                return KillParticipation;
            }

            if (TypeOfStat == StatDisplaySettings.StatType.EffectiveHealAndAbsorb)
            {
                return GetSupportPerTurn();
            }

            if (TypeOfStat == StatDisplaySettings.StatType.TeamDamageAdjustedByMe)
            {
                return GetTeamDamageSwingByMePerTurn();
            }

            if (TypeOfStat == StatDisplaySettings.StatType.TeamExtraEnergyByEnergizedFromMe)
            {
                return GetTeamEnergyBoostedByMePerTurn();
            }

            if (TypeOfStat == StatDisplaySettings.StatType.DamageTakenPerLife)
            {
                return GetDamageTakenPerLife();
            }

            if (TypeOfStat == StatDisplaySettings.StatType.EnemiesSightedPerLife)
            {
                return EnemiesSightedPerTurn;
            }

            if (TypeOfStat == StatDisplaySettings.StatType.TotalTurns)
            {
                return TotalGameTurns;
            }

            if (TypeOfStat == StatDisplaySettings.StatType.TotalTeamDamageReceived)
            {
                return TotalTeamDamageReceived;
            }

            if (TypeOfStat == StatDisplaySettings.StatType.TankingPerLife)
            {
                return GetTankingPerLife();
            }

            if (TypeOfStat == StatDisplaySettings.StatType.TeamMitigation)
            {
                return GetTeamMitigation();
            }

            if (TypeOfStat == StatDisplaySettings.StatType.SupportPerTurn)
            {
                return GetSupportPerTurn();
            }

            if (TypeOfStat == StatDisplaySettings.StatType.DamageDonePerLife)
            {
                return GetDamageDonePerLife();
            }

            if (TypeOfStat == StatDisplaySettings.StatType.DamageTakenPerTurn)
            {
                return GetDamageTakenPerTurn();
            }

            if (TypeOfStat == StatDisplaySettings.StatType.AvgLifeSpan)
            {
                return GetAvgLifeSpan();
            }

            return null;
        }

        public float? GetFreelancerStat(int FreelancerStatIndex)
        {
            if (-1 < FreelancerStatIndex && FreelancerStatIndex < FreelancerStats.Count)
            {
                return FreelancerStats[FreelancerStatIndex];
            }

            return null;
        }

        public long AccountId;
        public string InGameName;
        public CharacterType CharacterPlayed;
        public FreelancerSelectionOnus FreelancerSelectionOnus;
        public string CharacterName;
        public int CharacterSkinIndex;
        public int Team;
        public int TeamSlot;
        public int PlayerId;
        public string PrepCatalystName;
        public string DashCatalystName;
        public string CombatCatalystName;
        public bool PrepCatalystUsed;
        public bool DashCatalystUsed;
        public bool CombatCatalystUsed;
        public int PowerupsCollected;
        public PlayerGameResult PlayerGameResult = PlayerGameResult.NoResult;
        public MatchResultsStats MatchResults;
        public int TotalGameTurns;
        public int TotalPotentialAbsorb;
        public int TotalTechPointsGained;
        public int TotalHealingReceived;
        public int TotalAbsorbReceived;
        public int NumKills;
        public int NumDeaths;
        public int NumAssists;
        public int TotalPlayerDamage;
        public int TotalPlayerHealing;
        public int TotalPlayerAbsorb;
        public int TotalPlayerDamageReceived;
        public int TotalTeamDamageReceived;
        public int NetDamageAvoidedByEvades;
        public int DamageAvoidedByEvades;
        public int DamageInterceptedByEvades;
        public int MyIncomingDamageReducedByCover;
        public int MyOutgoingDamageReducedByCover;
        public int MyOutgoingExtraDamageFromEmpowered;
        public int MyOutgoingReducedDamageFromWeakened;
        public int TeamOutgoingDamageIncreasedByEmpoweredFromMe;
        public int TeamIncomingDamageReducedByWeakenedFromMe;
        public float MovementDeniedByMe;
        public float EnergyGainPerTurn;
        public float DamageEfficiency;
        public float KillParticipation;
        public int EffectiveHealing;
        public int EffectiveHealingFromAbility;
        public int TeamExtraEnergyByEnergizedFromMe;
        public float EnemiesSightedPerTurn;
        public int CharacterSpecificStat;
        public List<int> FreelancerStats = new List<int>();
        public int BaseISOGained;
        public int BaseFreelancerCurrencyGained;
        public int BaseXPGainedAccount;
        public int BaseXPGainedCharacter;
        public int WinFreelancerCurrencyGained;
        public int FirstWinFreelancerCurrencyGained;
        public int EventBonusFreelancerCurrencyGained;
        public int GGPacksSelfUsed;
        public int GGNonSelfCount;
        public int GGISOGained;
        public int GGFreelancerCurrencyGained;
        public int GGXPGainedAccount;
        public int GGXPGainedCharacter;
        public int ConsumableXPGainedCharacter;
        public int PlayWithFriendXPGainedAccount;
        public int PlayWithFriendXPGainedCharacter;
        public int QuestXPGainedAccount;
        public int QuestXPGainedCharacter;
        public int EventBonusXPGainedAccount;
        public int EventBonusXPGainedCharacter;
        public int FirstWinXpGainedAccount;
        public int FirstWinXpGainedCharacter;
        public int QueueTimeXpGainedAccount;
        public int QueueTimeXpGainedCharacter;
        public int FreelancerOwnedXPAmount;
        public int ActorIndex;
        public TimeSpan QueueTime;
        public List<AbilityGameSummary> AbilityGameSummaryList = new List<AbilityGameSummary>();
        public List<int> TimebankUsage = new List<int>();
        public Dictionary<string, float> AccountEloDeltas;
        public Dictionary<string, float> CharacterEloDeltas;
        public int RankedSortKarmaDelta;
        public float UsedMMR;
        public float TotalBadgePoints;
    }
}
