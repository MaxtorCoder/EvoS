using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.Game.GameManager
{
    [UNetMessage(serverMsgIds: new short[]{68})]
    public class EndGameNotification : MessageBase
    {
    }
}
