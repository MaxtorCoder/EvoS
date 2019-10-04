using System;
using System.Collections.Generic;

namespace EvoS.Framework.Assets
{
    public class TypeTree
    {
        public int Attributes { get; set; }
        public byte Embeded { get; set; }
        public List<TypeEntry> BaseClasses { get; set; } = new List<TypeEntry>();

        public TypeTree(StreamReader stream)
        {
            Attributes = stream.ReadInt32();
            Embeded = stream.ReadByte();
            if (Embeded != 0)
            {
                throw new NotImplementedException();
            }

            var baseClassCount = stream.ReadInt32();
            for (var i = 0; i < baseClassCount; i++)
            {
                var entry = new TypeEntry(stream) {Index = i};
                BaseClasses.Add(entry);
            }
        }

        public override string ToString()
        {
            return $"{nameof(TypeTree)}(" +
                   $"{nameof(Attributes)}: {Attributes}, " +
                   $"{nameof(Embeded)}: {Embeded}, " +
                   $"{nameof(BaseClasses)}: {String.Join(", ", BaseClasses)}" +
                   ")";
        }
    }

    public class TypeEntry
    {
        public int Index { get; set; }
        public int TypeId { get; set; }
        public byte Unknown1 { get; set; }
        public short Unknown2 { get; set; }
        public byte[]? Foo2 { get; set; }
        public byte[] Foo1 { get; set; }

        public TypeEntry(StreamReader stream)
        {
            TypeId = stream.ReadInt32();
            Unknown1 = stream.ReadByte();
            Unknown2 = stream.ReadInt16();
            if (TypeId == 114) // MonoBehaviour
            {
                Foo2 = stream.ReadBytes(16);
            }

            Foo1 = stream.ReadBytes(16);
        }

        public override string ToString()
        {
            return $"{nameof(TypeEntry)}(" +
                   $"{nameof(TypeId)}: {TypeId}, " +
                   $"{nameof(Unknown1)}: {Unknown1}, " +
                   $"{nameof(Unknown2)}: {Unknown2}, " +
//                   $"{nameof(Foo2)}: {Foo2}, " +
//                   $"{nameof(Foo1)}: {BitConverter.ToString(Foo1)}" +
                   ")";
        }
    }
}
