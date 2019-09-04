using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Framework.Network
{
    public class EvosMessageStreamException : Exception
    {
        public EvosMessageStreamException() { }

        public EvosMessageStreamException(String message) : base(message) { }
    }
}
