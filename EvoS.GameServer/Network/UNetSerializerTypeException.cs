using System;

namespace EvoS.GameServer.Network
{
    public class UNetSerializerTypeException : Exception
    {
        public UNetSerializerTypeException()
        {
        }

        public UNetSerializerTypeException(string message) : base(message)
        {
        }
    }
}