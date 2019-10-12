namespace EvoS.Framework.Network.Unity
{
    public class NetworkMessage
    {
        public const int MaxMessageSize = 65535;
        public uint msgSeqNum;
        public short msgType;
//        public NetworkConnection conn;
        public NetworkReader reader;
        public int channelId;

        public static string Dump(byte[] payload, int sz)
        {
            var str = "[";
            for (var index = 0; index < sz; ++index)
                str = str + payload[index] + " ";
            return str + "]";
        }

        public TMsg ReadMessage<TMsg>() where TMsg : MessageBase, new()
        {
            TMsg msg = new TMsg();
            msg.Deserialize(this.reader);
            return msg;
        }

        public void ReadMessage<TMsg>(TMsg msg) where TMsg : MessageBase
        {
            msg.Deserialize(this.reader);
        }
    }
}
