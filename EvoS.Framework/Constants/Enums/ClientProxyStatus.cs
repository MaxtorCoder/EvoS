using EvoS.Framework.Network;

namespace EvoS.Framework.Constants.Enums
{
    [EvosMessage(787)]
    public enum ClientProxyStatus
    {
        Unassigned,
        Assigned,
        Connected,
        Disconnected,
        Unloaded
    }
}
