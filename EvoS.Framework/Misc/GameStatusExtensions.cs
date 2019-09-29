using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Misc
{
    public static class GameStatusExtensions
    {
        public static bool IsActiveStatus(this GameStatus value)
        {
            return value >= GameStatus.Assembling && value <= GameStatus.Started;
        }

        public static bool IsPreLaunchStatus(this GameStatus value)
        {
            return value >= GameStatus.Assembling && value < GameStatus.Launching;
        }

        public static bool IsPostLaunchStatus(this GameStatus value)
        {
            return value >= GameStatus.Launching && value <= GameStatus.Stopped;
        }
    }
}
