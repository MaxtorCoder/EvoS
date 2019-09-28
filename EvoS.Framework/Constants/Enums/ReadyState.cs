using EvoS.Framework.Network;

namespace EvoS.Framework.Constants.Enums
{
    [EvosMessage(713)]
    public enum ReadyState
    {
        Unknown,
        Accepted,
        Declined,
        Ready
    }
}
