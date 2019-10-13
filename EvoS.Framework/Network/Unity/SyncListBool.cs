using System;

namespace EvoS.Framework.Network.Unity
{
    public class SyncListBool : SyncList<bool>
    {
        public override void SerializeItem(NetworkWriter writer, bool item)
        {
            writer.Write(item);
        }

        public override bool DeserializeItem(NetworkReader reader)
        {
            return reader.ReadBoolean();
        }

        [Obsolete("ReadReference is now used instead")]
        public static SyncListBool ReadInstance(NetworkReader reader)
        {
            var num = reader.ReadUInt16();
            var syncListBool = new SyncListBool();
            for (ushort index = 0; (int) index < (int) num; ++index)
                syncListBool.AddInternal(reader.ReadBoolean());
            return syncListBool;
        }

        public static void ReadReference(NetworkReader reader, SyncListBool syncList)
        {
            var num = reader.ReadUInt16();
            syncList.Clear();
            for (ushort index = 0; (int) index < (int) num; ++index)
                syncList.AddInternal(reader.ReadBoolean());
        }

        public static void WriteInstance(NetworkWriter writer, SyncListBool items)
        {
            writer.Write((ushort) items.Count);
            foreach (var item in items)
                writer.Write(item);
        }
    }
}
