using EvoS.Framework.Network;

namespace EvoS.Framework.Constants.Enums
{
    [EvosMessage(253)]
    public enum Team
    {
        Invalid = -1,
        TeamA,
        TeamB,
        Objects,
        Spectator
    }
}
