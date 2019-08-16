using System;
using System.IO;
using System.Text;
using vtortola.WebSockets;
using EvoS.Framework.Network.Static;

namespace EvoS.Framework.Network.WebSocket
{
    [Serializable]
    public abstract class WebSocketMessage
    {


        public static int MessageTypeID = 0;

        public const int INVALID_ID = 0;
        public int RequestId { get; set; }
        public int ResponseId { get; set; }

        [NonSerialized]
        public long DeserializationTicks;
        [NonSerialized]
        public long SerializedLength;

        public abstract void CreateFromStream(EvosMessageStream message);

        public void ReadHeader(EvosMessageStream message)
        {
            RequestId = message.ReadVarInt();
            ResponseId = message.ReadVarInt();

            Console.WriteLine("request: " + RequestId + ", response: " + ResponseId);
        }


    }
}
