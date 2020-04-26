using System;

namespace EvoS.Framework.Game
{
    public class EvoSGameConfig
    {
        public static bool AllowDebugCommands = true;
        public static bool NetworkIsServer = true;
        public static bool NetworkIsClient = false;
        public static bool DebugNetworkBehaviour = true;
        public static bool DebugSyncVars = false;
        public static bool DebugNetSerialize = false;
        public static bool UseDatabase = true;

        public static bool GameTypeAvailability_Practice = true;
        public static bool GameTypeAvailability_Coop = false;
        public static bool GameTypeAvailability_PvP = true;
        public static bool GameTypeAvailability_Ranked = false;
        public static bool GameTypeAvailability_Custom = false;

        public static TimeSpan LoadoutSelectTimeout = TimeSpan.FromSeconds(5);
    }
}
