using EvoS.Framework.Network;
using EvoS.Framework.Network.NetworkMessages;

namespace EvoS.Framework.Constants.Enums
{
    [EvosMessage(782)]
    public enum AuthType
    {
        Unknown,
        Ticket,
        FakeTicket,
        RequestTicket
    }
}
