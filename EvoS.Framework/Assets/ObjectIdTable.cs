using System;
using System.Collections.Generic;

namespace EvoS.Framework.Assets
{
    public class ObjectIdTable
    {
        public List<ObjectIdentifier> Entries { get; set; } = new List<ObjectIdentifier>();

        public ObjectIdTable(StreamReader stream)
        {
            var entryCount = stream.ReadInt32();
            for (var i = 0; i < entryCount; i++)
            {
                stream.AlignTo();

                Entries.Add(new ObjectIdentifier(stream));
            }
        }

        public override string ToString()
        {
            return $"{nameof(ObjectIdTable)}(" +
                   $"{nameof(Entries)}: {String.Join(", ", Entries)}" +
                   ")";
        }
    }

    public class ObjectIdentifier
    {
        public int SerializedFileIndex { get; set; }
        public ulong IdentifierInFile { get; set; }

        public ObjectIdentifier(StreamReader stream)
        {
            SerializedFileIndex = stream.ReadInt32();
            IdentifierInFile = stream.ReadUInt64();
        }

        public override string ToString()
        {
            return $"{nameof(ObjectIdentifier)}(" +
                   $"{nameof(SerializedFileIndex)}: {SerializedFileIndex}, " +
                   $"{nameof(IdentifierInFile)}: {IdentifierInFile}" +
                   ")";
        }
    }
}
