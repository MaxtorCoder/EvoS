using EvoS.Framework.Network.Unity;

namespace EvoS.GameServer.Network.Messages.Unity
{
    [UNetMessage(serverMsgIds: new short[] {13})]
    public class ObjectDestroyMessage : MessageBase
    {
        public override void Deserialize(NetworkReader reader)
        {
            netId = reader.ReadNetworkId();
        }

        public override void Serialize(NetworkWriter writer)
        {
            writer.Write(netId);
        }

        public NetworkInstanceId netId;
    }
}
