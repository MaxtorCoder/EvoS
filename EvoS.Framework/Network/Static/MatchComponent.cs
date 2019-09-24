using System;
using System.Collections.Generic;
using System.Linq;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(612)]
    public class MatchComponent : ICloneable
    {
        public DateTime MatchTime { get; set; }

        public PlayerGameResult Result { get; set; }

        public int Kills { get; set; }

        public CharacterType CharacterUsed { get; set; }

        public GameType GameType { get; set; }

        public string MapName { get; set; }

        public int NumOfTurns { get; set; }

        public string SubTypeLocTag { get; set; }

        public CharacterType GetFirstPlayerCharacter()
        {
            if (Actors == null)
            {
                return CharacterUsed;
            }

            for (int i = 0; i < Actors.Count; i++)
            {
                if (Actors[i].IsPlayer)
                {
                    return Actors[i].Character;
                }
            }

            return CharacterUsed;
        }

//        public string GetTimeDifferenceText()
//        {
//            TimeSpan difference = DateTime.UtcNow - MatchTime;
//            return StringUtil.GetTimeDifferenceText(difference, false);
//        }

        public string GetSubTypeNameTerm()
        {
            string text = (SubTypeLocTag == null) ? "unknown" : SubTypeLocTag.Split("@".ToCharArray()).First();
            if (text == "unknown")
            {
                GameType gameType = GameType;
                switch (gameType)
                {
                    case GameType.Custom:
                        text = "GenericCustom";
                        break;
                    case GameType.Practice:
                        text = "GenericPractice";
                        break;
                    case GameType.Tutorial:
                        text = "GenericTutorial";
                        break;
                    default:
                        switch (gameType)
                        {
                            case GameType.Ranked:
                                text = "GenericRanked";
                                break;
                            case GameType.NewPlayerSolo:
                                text = "GenericNewPlayerSolo";
                                break;
                        }

                        break;
                }
            }

            if (text == "unknown" && MapName.EndsWith("CTF"))
            {
                text = "GenericBriefcase";
            }

            return text;
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        [EvosMessage(613)]
        public List<Actor> Actors = new List<Actor>();

        [Serializable]
        [EvosMessage(615)]
        public struct Actor
        {
            public CharacterType Character;

            public Team Team;

            public bool IsPlayer;
        }
    }
}
