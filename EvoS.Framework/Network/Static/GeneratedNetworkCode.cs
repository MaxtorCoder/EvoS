using EvoS.Framework.Misc;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Network.Static
{
    public class GeneratedNetworkCode
    {
        public static void _ReadStructSyncListTempCoverInfo_None(
            NetworkReader reader,
            SyncListTempCoverInfo instance)
        {
            var num = reader.ReadUInt16();
            instance.Clear();
            for (ushort index = 0; (int) index < (int) num; ++index)
                instance.AddInternal(instance.DeserializeItem(reader));
        }

        public static void _WriteStructSyncListTempCoverInfo_None(
            NetworkWriter writer,
            SyncListTempCoverInfo value)
        {
            var count = value.Count;
            writer.Write(count);
            for (ushort index = 0; (int) index < (int) count; ++index)
                value.SerializeItem(writer, value.GetItem(index));
        }

        public static void _ReadStructSyncListVisionProviderInfo_None(
            NetworkReader reader,
            SyncListVisionProviderInfo instance)
        {
            var num = reader.ReadUInt16();
            instance.Clear();
            for (ushort index = 0; (int) index < (int) num; ++index)
                instance.AddInternal(instance.DeserializeItem(reader));
        }

        public static void _WriteStructSyncListVisionProviderInfo_None(
            NetworkWriter writer,
            SyncListVisionProviderInfo value)
        {
            var count = value.Count;
            writer.Write(count);
            for (ushort index = 0; (int) index < (int) count; ++index)
                value.SerializeItem(writer, value.GetItem(index));
        }

        public static GridPosProp _ReadGridPosProp_None(NetworkReader reader)
        {
            return new GridPosProp()
            {
                m_x = (int) reader.ReadPackedUInt32(),
                m_y = (int) reader.ReadPackedUInt32(),
                m_height = (int) reader.ReadPackedUInt32()
            };
        }

        public static void _WriteGridPosProp_None(NetworkWriter writer, GridPosProp value)
        {
            writer.WritePackedUInt32((uint) value.m_x);
            writer.WritePackedUInt32((uint) value.m_y);
            writer.WritePackedUInt32((uint) value.m_height);
        }
    }
}
