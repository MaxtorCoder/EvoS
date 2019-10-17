using System;

namespace EvoS.Framework.Assets.Serialized.Behaviours
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SerializedMonoBehaviourAttribute : Attribute
    {
        public string ClassName { get; }

        public SerializedMonoBehaviourAttribute(string className)
        {
            ClassName = className.Contains(".") ? className : '.' + className;
        }
    }
}
