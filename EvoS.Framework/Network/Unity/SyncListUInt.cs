using System;

namespace EvoS.Framework.Network.Unity
{
    public class SyncListUInt : SyncList<uint>
    {
        public override void SerializeItem(NetworkWriter writer, uint item)
        {
            writer.WritePackedUInt32(item);
        }

        public override uint DeserializeItem(NetworkReader reader)
        {
            return reader.ReadPackedUInt32();
        }

        [Obsolete("ReadReference is now used instead")]
        public static SyncListUInt ReadInstance(NetworkReader reader)
        {
            var num = reader.ReadUInt16();
            var syncListUint = new SyncListUInt();
            for (ushort index = 0; (int) index < (int) num; ++index)
                syncListUint.AddInternal(reader.ReadPackedUInt32());
            return syncListUint;
        }

        public static void ReadReference(NetworkReader reader, SyncListUInt syncList)
        {
            var num = reader.ReadUInt16();
            syncList.Clear();
            for (ushort index = 0; (int) index < (int) num; ++index)
                syncList.AddInternal(reader.ReadPackedUInt32());
        }

        public static void WriteInstance(NetworkWriter writer, SyncListUInt items)
        {
            writer.Write((ushort) items.Count);
            foreach (var item in items)
                writer.WritePackedUInt32(item);
        }
    }
}
