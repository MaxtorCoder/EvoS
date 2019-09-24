using EvoS.Framework.Network;

namespace EvoS.Framework.Constants.Enums
{
    [EvosMessage(616)]
    public enum PlayerGameResult
    {
        NoResult = -1,
        Tie,
        Win,
        Lose
    }
}
