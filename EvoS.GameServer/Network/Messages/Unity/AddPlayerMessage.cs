using EvoS.Framework.Network.Unity;

namespace EvoS.GameServer.Network.Messages.Unity
{
    [UNetMessage(clientMsgIds: new short[] {37})]
    public class AddPlayerMessage : MessageBase
    {
        public short playerControllerId;
        public int msgSize;
        public byte[] msgData;

        public override void Deserialize(NetworkReader reader)
        {
            playerControllerId = (short) reader.ReadUInt16();
            msgData = reader.ReadBytesAndSize();
            msgSize = msgData?.Length ?? 0;
        }

        public override void Serialize(NetworkWriter writer)
        {
            writer.Write((ushort) playerControllerId);
            writer.WriteBytesAndSize(msgData, msgSize);
        }
    }
}
