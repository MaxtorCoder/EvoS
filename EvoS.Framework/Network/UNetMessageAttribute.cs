using System;

namespace EvoS.Framework.Network
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class UNetMessageAttribute : Attribute
    {
        public short[] ServerMsgIds { get; }
        public short[] ClientMsgIds { get; }

        public UNetMessageAttribute(short[] serverMsgIds = null, short[] clientMsgIds = null)
        {
            ServerMsgIds = serverMsgIds ?? new short[0];
            ClientMsgIds = clientMsgIds ?? new short[0];
        }
    }
}
