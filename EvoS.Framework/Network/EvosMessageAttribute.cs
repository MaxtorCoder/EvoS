using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Framework.Network
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EvosMessageAttribute : Attribute
    {
        public int TypeID { get; set; }
        public Type Type { get; set; }

        public EvosMessageAttribute(int typeId, Type type = null)
        {
            TypeID = typeId;
            Type = type;
        }
    }
}
