using EvoS.Framework.Network;

namespace EvoS.Framework.Constants.Enums
{
    [EvosMessage(252)]
    public enum TopParticipantSlot
    {
        ERROR,
        Deadliest,
        Supportiest,
        Tankiest,
        MostDecorated
    }
}
