using System;

namespace EvoS.Framework.Network.Unity
{
    public sealed class SyncListFloat : SyncList<float>
    {
        public override void SerializeItem(NetworkWriter writer, float item)
        {
            writer.Write(item);
        }

        public override float DeserializeItem(NetworkReader reader)
        {
            return reader.ReadSingle();
        }

        [Obsolete("ReadReference is now used instead")]
        public static SyncListFloat ReadInstance(NetworkReader reader)
        {
            var num = reader.ReadUInt16();
            var syncListFloat = new SyncListFloat();
            for (ushort index = 0; (int) index < (int) num; ++index)
                syncListFloat.AddInternal(reader.ReadSingle());
            return syncListFloat;
        }

        public static void ReadReference(NetworkReader reader, SyncListFloat syncList)
        {
            var num = reader.ReadUInt16();
            syncList.Clear();
            for (ushort index = 0; (int) index < (int) num; ++index)
                syncList.AddInternal(reader.ReadSingle());
        }

        public static void WriteInstance(NetworkWriter writer, SyncListFloat items)
        {
            writer.Write((ushort) items.Count);
            foreach (var t in items)
                writer.Write(t);
        }
    }
}
