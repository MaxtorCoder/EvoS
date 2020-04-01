using System;
using System.Collections;
using System.Collections.Generic;
using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Misc;
using Newtonsoft.Json;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(506)]
    public class ExperienceComponent : ICloneable
    {
        public ExperienceComponent()
        {
            this.Level = 1;
            this.TutorialProgress = TutorialVersion.Latest;
            this.EloValues = new EloValues();
            this.PersistedStatsDictionary = new Dictionary<PersistedStatBucket, PersistedStats>();
            this.PersistedStatsDictionaryBySeason =
                new Dictionary<int, Dictionary<PersistedStatBucket, PersistedStats>>();
            this.BadgesEarned = new Dictionary<PersistedStatBucket, Dictionary<int, int>>();
            this.BadgesEarnedBySeason = new Dictionary<int, Dictionary<PersistedStatBucket, Dictionary<int, int>>>();
            this.MatchesPerGameType = new Dictionary<GameType, int>(default(GameTypeComparer));
            this.WinsPerGameType = new Dictionary<GameType, int>(default(GameTypeComparer));
        }

        public int Level { get; set; }

        public int XPProgressThroughLevel { get; set; }

        public int EnteredTutorial { get; set; }

        public TutorialVersion TutorialProgress { get; set; }

        public int Matches { get; set; }

        public int Wins { get; set; }

        public int Kills { get; set; }

        public int LossStreak { get; set; }

        public EloValues EloValues { get; set; }

        public DateTime LastWin { get; set; }

        [EvosMessage(513)]
        public Dictionary<PersistedStatBucket, PersistedStats> PersistedStatsDictionary { get; set; }

        [EvosMessage(510)]
        public Dictionary<int, Dictionary<PersistedStatBucket, PersistedStats>> PersistedStatsDictionaryBySeason
        {
            get;
            set;
        }

        [EvosMessage(507)]
        public Dictionary<GameType, int> MatchesPerGameType { get; set; }

        public Dictionary<GameType, int> WinsPerGameType { get; set; }

        [EvosMessage(524)]
        public Dictionary<PersistedStatBucket, Dictionary<int, int>> BadgesEarned { get; set; }

        [EvosMessage(521)]
        public Dictionary<int, Dictionary<PersistedStatBucket, Dictionary<int, int>>> BadgesEarnedBySeason { get; set; }

        public object Clone()
        {
            return base.MemberwiseClone();
        }

        [JsonIgnore]
        public int VsHumanMatches
        {
            get
            {
                int num = 0;
                for (int i = 0; i < 16; i++)
                {
                    GameType gameType = (GameType) i;
                    int num2;
                    if (gameType.IsHumanVsHumanGame() && this.MatchesPerGameType.TryGetValue(gameType, out num2))
                    {
                        num += num2;
                    }
                }

                return num;
            }
        }

        [JsonIgnore]
        public int VsHumanWins
        {
            get
            {
                int num = 0;
                IEnumerator enumerator = Enum.GetValues(typeof(GameType)).GetEnumerator();
                try
                {
                    while (enumerator.MoveNext())
                    {
                        object obj = enumerator.Current;
                        GameType gameType = (GameType) obj;
                        int num2;
                        if (gameType.IsHumanVsHumanGame() && this.WinsPerGameType.TryGetValue(gameType, out num2))
                        {
                            num += num2;
                        }
                    }
                }
                finally
                {
                    IDisposable disposable;
                    if ((disposable = (enumerator as IDisposable)) != null)
                    {
                        disposable.Dispose();
                    }
                }

                return num;
            }
        }

        [JsonIgnore]
        public int Losses => this.Matches - this.Wins;

//        [JsonIgnore]
//        public int XPToNextLevel
//        {
//            get
//            {
//                GameBalanceVars gameBalanceVars = GameBalanceVars.Get();
//                return gameBalanceVars.CharacterExperienceToLevel(this.Level);
//            }
//        }
    }
}
