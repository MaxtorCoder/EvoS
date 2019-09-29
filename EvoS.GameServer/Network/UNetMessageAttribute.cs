using System;

namespace EvoS.GameServer.Network
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class UNetMessageAttribute : Attribute
    {
        public int[] MsgIds { get; }

        public UNetMessageAttribute(params int[] msgIds)
        {
            MsgIds = msgIds;
        }
    }
}
