using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Framework.Network
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Field |
                    AttributeTargets.Property | AttributeTargets.Struct)]
    public class EvosMessageAttribute : Attribute
    {
        public int TypeID { get; set; }
        public Type Type { get; set; }
        public bool IgnoreGenericArgs { get; set; }

        public EvosMessageAttribute(int typeId, Type type = null, bool ignoreGenericArgs = false)
        {
            TypeID = typeId;
            Type = type;
            IgnoreGenericArgs = ignoreGenericArgs;
        }
    }
}
