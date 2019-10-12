using System;
using System.Collections.Generic;

namespace EvoS.Framework.Assets
{
    public class ObjectInfoTable : Dictionary<long, ObjectInfoEntry>
    {
        public List<ObjectInfoEntry> Entries { get; set; } = new List<ObjectInfoEntry>();

        public ObjectInfoTable(StreamReader stream)
        {
            var entryCount = stream.ReadInt32();
            for (var i = 0; i < entryCount; i++)
            {
                stream.AlignTo();

                var entry = new ObjectInfoEntry(stream);
                Entries.Add(entry);
                Add(entry.PathId, entry);
            }
        }

        public override string ToString()
        {
            return $"{nameof(ObjectInfoTable)}(" +
                   $"{nameof(Entries)}: {String.Join(", ", Entries)}" +
                   ")";
        }
    }

    public class ObjectInfoEntry
    {
        public long PathId { get; set; }
        public uint ByteStart { get; set; }
        public uint ByteSize { get; set; }
        public int TypeId { get; set; }

        public ObjectInfoEntry(StreamReader stream)
        {
            PathId = stream.ReadInt64();

            ByteStart = stream.ReadUInt32();
            ByteSize = stream.ReadUInt32();
            TypeId = stream.ReadInt32();
        }

        public override string ToString()
        {
            return $"{nameof(ObjectInfoEntry)}(" +
                   $"{nameof(PathId)}: {PathId}, " +
                   $"{nameof(ByteStart)}: {ByteStart}, " +
                   $"{nameof(ByteSize)}: {ByteSize}, " +
                   $"{nameof(TypeId)}: {TypeId}" +
                   ")";
        }
    }
}
