using System;

namespace EvoS.Framework.Network
{
    public class EvosSerializerTypeException : Exception
    {
        public EvosSerializerTypeException()
        {
        }

        public EvosSerializerTypeException(string message) : base(message)
        {
        }
    }
}