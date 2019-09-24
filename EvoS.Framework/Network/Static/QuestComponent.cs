using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(562)]
    public class QuestComponent
    {
        public QuestComponent()
        {
            Progress = new Dictionary<int, QuestProgress>();
            StartOfMostRecentDailyList = DateTime.MinValue;
            StartOfMostRecentDailyPick = DateTime.MinValue;
            OfferedDailies = new List<int>();
            QuestMetaDatas = new Dictionary<int, QuestMetaData>();
            ActiveSeason = 0;
            SeasonExperience = new Dictionary<int, ExperienceComponent>();
            UnlockedSeasonChapters = new Dictionary<int, List<int>>();
            NotifiedSeasonChapters = new Dictionary<int, List<int>>();
            CompletedSeasonChapters = new Dictionary<int, List<int>>();
            SeasonRewardVersion = new Dictionary<int, int>();
            FactionCompetitionLoginRewardVersion = new Dictionary<int, int>();
            FactionCompetitionRewards = new Dictionary<int, Dictionary<int, FactionTierAndVersion>>();
            NotifySeasonEnded = 0;
            SeasonItemRewardsGranted = new Dictionary<int, List<int>>();
        }

        [EvosMessage(572)]
        public Dictionary<int, QuestProgress> Progress { get; set; }

        public DateTime StartOfMostRecentDailyPick { get; set; }

        public DateTime StartOfMostRecentDailyList { get; set; }

        public List<int> OfferedDailies { get; set; }

        [EvosMessage(566)]
        public Dictionary<int, QuestMetaData> QuestMetaDatas { get; set; }

        public int ActiveSeason { get; set; }

        [EvosMessage(563)]
        public Dictionary<int, ExperienceComponent> SeasonExperience { get; set; }

        [EvosMessage(7)]
        public Dictionary<int, List<int>> UnlockedSeasonChapters { get; set; }

        public Dictionary<int, List<int>> CompletedSeasonChapters { get; set; }

        public Dictionary<int, List<int>> NotifiedSeasonChapters { get; set; }

        [EvosMessage(270)]
        public Dictionary<int, int> SeasonRewardVersion { get; set; }

        public Dictionary<int, int> FactionCompetitionLoginRewardVersion { get; set; }

        [EvosMessage(579)]
        public Dictionary<int, Dictionary<int, FactionTierAndVersion>> FactionCompetitionRewards { get; set; }

        public int NotifySeasonEnded { get; set; }

        public int NotifiedChapterStarted { get; set; }

        public DateTime LastChapterUnlockNotification { get; set; }

        public Dictionary<int, List<int>> SeasonItemRewardsGranted { get; set; }

        [Obsolete] public Dictionary<int, int> CompletedQuests { get; set; }

        [Obsolete] public Dictionary<int, int> RejectedQuestCount { get; set; }

        [Obsolete] public Dictionary<int, int> AbandonedQuests { get; set; }

        public List<int> GetUnlockedSeasonChapters(int season)
        {
            if (UnlockedSeasonChapters.ContainsKey(season))
            {
                return UnlockedSeasonChapters[season];
            }

            return new List<int>();
        }

        public List<int> GetCompletedSeasonChapters(int season)
        {
            if (CompletedSeasonChapters.ContainsKey(season))
            {
                return CompletedSeasonChapters[season];
            }

            return new List<int>();
        }

        public ExperienceComponent GetSeasonExperienceComponent(int season)
        {
            if (SeasonExperience.ContainsKey(season))
            {
                return SeasonExperience[season];
            }

            return new ExperienceComponent();
        }

        public int GetCompletedCount(int questId)
        {
            QuestMetaData questMetaData;
            if (QuestMetaDatas != null && QuestMetaDatas.TryGetValue(questId, out questMetaData))
            {
                return questMetaData.CompletedCount;
            }

            return 0;
        }

        public int GetRejectedCount(int questId)
        {
            QuestMetaData questMetaData;
            if (QuestMetaDatas != null && QuestMetaDatas.TryGetValue(questId, out questMetaData))
            {
                return questMetaData.RejectedCount;
            }

            return 0;
        }

        public int GetAbandonedCount(int questId)
        {
            QuestMetaData questMetaData;
            if (QuestMetaDatas != null && QuestMetaDatas.TryGetValue(questId, out questMetaData))
            {
                return questMetaData.AbandonedCount;
            }

            return 0;
        }

        public int GetWeight(int questId)
        {
            QuestMetaData questMetaData;
            if (QuestMetaDatas != null && QuestMetaDatas.TryGetValue(questId, out questMetaData))
            {
                return questMetaData.Weight;
            }

            return 0;
        }

        public QuestMetaData GetOrCreateQuestMetaData(int questId)
        {
            QuestMetaData questMetaData;
            if (!QuestMetaDatas.TryGetValue(questId, out questMetaData))
            {
                questMetaData = new QuestMetaData();
                questMetaData.UtcCompletedTimes = new List<DateTime>();
                QuestMetaDatas[questId] = questMetaData;
            }

            return questMetaData;
        }

        public void SetCompletedCount(int questId, int count)
        {
            QuestMetaData orCreateQuestMetaData = GetOrCreateQuestMetaData(questId);
            orCreateQuestMetaData.CompletedCount = count;
            orCreateQuestMetaData.PstAbandonDate = null;
        }

        public void SetRejectedCount(int questId, int count)
        {
            GetOrCreateQuestMetaData(questId).RejectedCount = count;
        }

        public void SetAbandonedCount(int questId, int count)
        {
            QuestMetaData orCreateQuestMetaData = GetOrCreateQuestMetaData(questId);
            orCreateQuestMetaData.AbandonedCount = count;
            orCreateQuestMetaData.PstAbandonDate = null;
        }

        public void SetWeight(int questId, int weight)
        {
            GetOrCreateQuestMetaData(questId).Weight = weight;
        }

        public void SetCompletedDate(int questId, DateTime utcTime)
        {
            QuestMetaData orCreateQuestMetaData = GetOrCreateQuestMetaData(questId);
            if (orCreateQuestMetaData.UtcCompletedTimes == null)
            {
                orCreateQuestMetaData.UtcCompletedTimes = new List<DateTime>();
            }

            orCreateQuestMetaData.UtcCompletedTimes.Add(utcTime);
        }

//        public int GetReactorLevel(List<SeasonTemplate> seasons)
//        {
//            int num = 0;
//            foreach (KeyValuePair<int, ExperienceComponent> keyValuePair in SeasonExperience)
//            {
//                for (int i = 0; i < seasons.Count; i++)
//                {
//                    if (seasons[i].Index == keyValuePair.Key)
//                    {
//                        if (!seasons[i].IsTutorial)
//                        {
//                            num += keyValuePair.Value.Level;
//                        }
//
//                        break;
//                    }
//                }
//            }
//
//            return num;
//        }

        public void GrantedSeasonItemReward(int seasonLevel, int itemTemplateId)
        {
            if (!SeasonItemRewardsGranted.ContainsKey(seasonLevel))
            {
                SeasonItemRewardsGranted.Add(seasonLevel, new List<int>());
            }

            SeasonItemRewardsGranted[seasonLevel].Add(itemTemplateId);
        }

        [JsonIgnore]
        public int SeasonLevel
        {
            get
            {
                if (ActiveSeason == 0)
                {
                    return 0;
                }

                if (SeasonExperience.ContainsKey(ActiveSeason))
                {
                    return SeasonExperience[ActiveSeason].Level;
                }

                return 1;
            }
            set
            {
                if (ActiveSeason == 0)
                {
                    return;
                }

                if (value < SeasonExperience[ActiveSeason].Level)
                {
                    for (int i = SeasonExperience[ActiveSeason].Level; i > value; i--)
                    {
                        SeasonItemRewardsGranted.Remove(i);
                    }
                }

                SeasonExperience[ActiveSeason].Level = value;
            }
        }

        [JsonIgnore]
        public int SeasonXPProgressThroughLevel
        {
            get
            {
                if (ActiveSeason == 0)
                {
                    return 0;
                }

                return SeasonExperience[ActiveSeason].XPProgressThroughLevel;
            }
            set
            {
                if (ActiveSeason == 0)
                {
                    return;
                }

                SeasonExperience[ActiveSeason].XPProgressThroughLevel = value;
            }
        }

        [JsonIgnore]
        public int HighestSeasonChapter
        {
            get
            {
                int num = 0;
                if (ActiveSeason == 0)
                {
                    return num;
                }

                if (GetUnlockedSeasonChapters(ActiveSeason) == null)
                {
                    return 1;
                }

                foreach (int num2 in GetUnlockedSeasonChapters(ActiveSeason))
                {
                    if (num2 > num)
                    {
                        num = num2;
                    }
                }

                return num + 1;
            }
            set
            {
                if (ActiveSeason == 0)
                {
                    return;
                }

                if (value < 1)
                {
                    return;
                }

                if (!UnlockedSeasonChapters.ContainsKey(ActiveSeason))
                {
                    UnlockedSeasonChapters[ActiveSeason] = new List<int>();
                }

                UnlockedSeasonChapters[ActiveSeason].Clear();
                for (int i = 0; i < value; i++)
                {
                    UnlockedSeasonChapters[ActiveSeason].Add(i);
                }
            }
        }
    }
}
