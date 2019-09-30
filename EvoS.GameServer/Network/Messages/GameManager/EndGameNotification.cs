using EvoS.GameServer.Network.Unity;

namespace EvoS.GameServer.Network.Messages.GameManager
{
    [UNetMessage(serverMsgIds: new short[]{68})]
    public class EndGameNotification : MessageBase
    {
    }
}
