namespace EvoS.Framework.Misc
{
    public static class GameTypeExtensions
    {
        public static bool IsHumanVsHumanGame(this GameType gameType)
        {
            return gameType == GameType.PvP || gameType == GameType.Ranked || gameType == GameType.NewPlayerPvP;
        }

        public static bool IsQueueable(this GameType gameType)
        {
            return gameType == GameType.Coop || gameType == GameType.PvP || gameType == GameType.Ranked ||
                   gameType == GameType.NewPlayerPvP;
        }

        public static bool IsAutoLaunchable(this GameType gameType)
        {
            return gameType == GameType.Custom || gameType == GameType.Practice || gameType == GameType.Tutorial;
        }

        public static bool TracksElo(this GameType gameType)
        {
            return gameType == GameType.PvP || gameType == GameType.Ranked || gameType == GameType.NewPlayerPvP;
        }

        public static bool AllowsLockedCharacters(this GameType gameType)
        {
            return gameType == GameType.Practice || gameType == GameType.Tutorial || gameType == GameType.NewPlayerSolo;
        }

        public static bool AllowsReconnect(this GameType gameType)
        {
            return gameType == GameType.Coop || gameType == GameType.PvP || gameType == GameType.Ranked ||
                   gameType == GameType.NewPlayerPvP || gameType == GameType.Custom;
        }

        public static string GetDisplayName(this GameType gameType)
        {
            switch (gameType)
            {
                case GameType.Custom:
                    return "Custom";
                case GameType.Practice:
                    return "Practice";
                case GameType.Tutorial:
                    return "Tutorial";
                case GameType.Coop:
                    return "VersusBots";
                case GameType.PvP:
                case GameType.NewPlayerPvP:
                    return "PVP";
                case GameType.Solo:
                case GameType.PvE:
                case GameType.NewPlayerSolo:
                    return "PvE";
                case GameType.Duel:
                    return "Duel";
                case GameType.QuickPlay:
                    return "QuickPlay";
                case GameType.Ranked:
                    return "Ranked";
            }

            return gameType + "#NotLocalized";
        }
    }
}
