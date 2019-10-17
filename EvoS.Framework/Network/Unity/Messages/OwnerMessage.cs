namespace EvoS.Framework.Network.Unity.Messages
{
    public class OwnerMessage : MessageBase
    {
        public NetworkInstanceId netId;
        public short playerControllerId;

        public override void Deserialize(NetworkReader reader)
        {
            netId = reader.ReadNetworkId();
            playerControllerId = (short) reader.ReadPackedUInt32();
        }

        public override void Serialize(NetworkWriter writer)
        {
            writer.Write(netId);
            writer.WritePackedUInt32((uint) playerControllerId);
        }
    }
}
