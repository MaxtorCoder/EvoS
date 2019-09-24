using System;
using System.Collections.Generic;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Misc
{
    [Serializable]
    public class QuestCondition
    {
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            QuestCondition questCondition = obj as QuestCondition;
            if (questCondition == null || ConditionType != questCondition.ConditionType)
            {
                return false;
            }

            switch (ConditionType)
            {
                case QuestConditionType.HasCompletedQuest:
                case QuestConditionType.HasUnlockedCharacter:
                case QuestConditionType.UsingCharacter:
                case QuestConditionType.UsingCharacterRole:
                case QuestConditionType.HasFriendCountOnTeam:
                case QuestConditionType.HasFriendCountOrMore:
                case QuestConditionType.UsingGameType:
                case QuestConditionType.HasAccountLevel:
                case QuestConditionType.UsingBanner:
                case QuestConditionType.UsingEmblem:
                case QuestConditionType.UsingTitle:
                case QuestConditionType.UsingItemTemplateId:
                case QuestConditionType.UsingItemType:
                case QuestConditionType.UsingItemRarity:
                case QuestConditionType.UsingCatalyst:
                case QuestConditionType.UsingSkinId:
                case QuestConditionType.UsingPatternId:
                case QuestConditionType.UsingColorId:
                case QuestConditionType.HasSeasonAccess:
                case QuestConditionType.UsingQuest:
                case QuestConditionType.HasQuestBonusLevel:
                case QuestConditionType.HasAbilityNumber:
                case QuestConditionType.DaysSinceLastProgress:
                case QuestConditionType.UsedGG:
                case QuestConditionType.HasEnemyTeamTitle:
                case QuestConditionType.HasEnemyTeamEmblem:
                case QuestConditionType.HasEnemyTeamBanner:
                case QuestConditionType.UsingStyleGroup:
                case QuestConditionType.HasUnlockedTitle:
                case QuestConditionType.HasUnlockedEmblem:
                case QuestConditionType.HasUnlockedBanner:
                case QuestConditionType.IsFactionCompetitionActive:
                case QuestConditionType.IsFreelancerAvailable:
                case QuestConditionType.UsingMap:
                case QuestConditionType.CurrencyType:
                case QuestConditionType.UsingStatBucket:
                case QuestConditionType.QueueGroupSize:
                case QuestConditionType.IsFreelancerPlayable:
                case QuestConditionType.BadgeEarned:
                case QuestConditionType.HasUnlockedOvercon:
                case QuestConditionType.HasUnlockedChatEmoji:
                    return typeSpecificData == questCondition.typeSpecificData;
                case QuestConditionType.HasCharacterLevel:
                case QuestConditionType.HasSeasonAndChapterAccess:
                case QuestConditionType.HasSeasonLevel:
                case QuestConditionType.HasCharacterElo:
                case QuestConditionType.UsedAbilityCount:
                case QuestConditionType.UsingCharacterFaction:
                case QuestConditionType.HasFriendCountInMatch:
                case QuestConditionType.UsingFactionBanner:
                case QuestConditionType.UsingFactionRibbon:
                case QuestConditionType.UsedOvercon:
                case QuestConditionType.HasUnlockedTaunt:
                    return typeSpecificData == questCondition.typeSpecificData &&
                           typeSpecificData2 == questCondition.typeSpecificData2;
                case QuestConditionType.HasDateTimePassed:
                    if (typeSpecificDate.Count != questCondition.typeSpecificDate.Count)
                    {
                        return false;
                    }

                    for (int i = 0; i < typeSpecificDate.Count; i++)
                    {
                        if (typeSpecificDate[i] != questCondition.typeSpecificDate[i])
                        {
                            return false;
                        }
                    }

                    return true;
                case QuestConditionType.HasTitleCountInMatch:
                case QuestConditionType.UsingVisualInfo:
                case QuestConditionType.HasBannerCountInMatch:
                case QuestConditionType.HasUnlockedStyle:
                    return typeSpecificData == questCondition.typeSpecificData &&
                           typeSpecificData2 == questCondition.typeSpecificData2 &&
                           typeSpecificData3 == questCondition.typeSpecificData3 &&
                           typeSpecificData4 == questCondition.typeSpecificData4;
                case QuestConditionType.UsingCommonGameTypes:
                case QuestConditionType.UsingQueuedGameTypes:
                    return true;
                case QuestConditionType.FactionTierReached:
                    return typeSpecificData == questCondition.typeSpecificData &&
                           typeSpecificData2 == questCondition.typeSpecificData2 &&
                           typeSpecificData3 == questCondition.typeSpecificData3;
            }

            return false;
        }

        public QuestConditionType ConditionType;
        public int typeSpecificData;
        public int typeSpecificData2;
        public int typeSpecificData3;
        public int typeSpecificData4;
        public List<int> typeSpecificDate;
        public string typeSpecificString;
    }
}
