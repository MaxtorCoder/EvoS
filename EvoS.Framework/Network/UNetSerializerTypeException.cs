using System;

namespace EvoS.Framework.Network
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