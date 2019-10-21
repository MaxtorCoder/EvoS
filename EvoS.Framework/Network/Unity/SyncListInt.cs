using System;

namespace EvoS.Framework.Network.Unity
{
    public class SyncListInt : SyncList<int>
    {
        public override void SerializeItem(NetworkWriter writer, int item)
        {
            writer.WritePackedUInt32((uint) item);
        }

        public override int DeserializeItem(NetworkReader reader)
        {
            return (int) reader.ReadPackedUInt32();
        }

        [Obsolete("ReadReference is now used instead")]
        public static SyncListInt ReadInstance(NetworkReader reader)
        {
            var num = reader.ReadUInt16();
            var syncListInt = new SyncListInt();
            for (ushort index = 0; (int) index < (int) num; ++index)
                syncListInt.AddInternal((int) reader.ReadPackedUInt32());
            return syncListInt;
        }

        public static void ReadReference(NetworkReader reader, SyncListInt syncList)
        {
            ushort num = reader.ReadUInt16();
            syncList.Clear();
            for (ushort index = 0; (int) index < (int) num; ++index)
                syncList.AddInternal((int) reader.ReadPackedUInt32());
        }

        public static void WriteInstance(NetworkWriter writer, SyncListInt items)
        {
            writer.Write((ushort) items.Count);
            foreach (var item in items)
                writer.WritePackedUInt32((uint) item);
        }
    }
}
