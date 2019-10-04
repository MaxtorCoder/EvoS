namespace EvoS.Framework.Assets
{
    public class SerializedFileHeader
    {
        public int MetadataSize { get; set; }
        public int FileSizeUnconfirmed { get; set; }
        public int FileVersion { get; set; }
        public int DataOffset { get; set; }
        public int Endianness { get; set; }
        public string Version { get; set; }

        public SerializedFileHeader(StreamReader stream)
        {
            var originalEndianness = stream.ByteOrder;
            stream.ByteOrder = ByteOrder.BigEndian;

            MetadataSize = stream.ReadInt32();
            FileSizeUnconfirmed = stream.ReadInt32();
            FileVersion = stream.ReadInt32();
            DataOffset = stream.ReadInt32();
            Endianness = stream.ReadInt32();
            Version = stream.ReadNullString();

            stream.ByteOrder = originalEndianness;
        }

        public override string ToString()
        {
            return $"{nameof(SerializedFileHeader)}(" +
                   $"{nameof(MetadataSize)}: {MetadataSize}, " +
                   $"{nameof(FileSizeUnconfirmed)}: {FileSizeUnconfirmed}, " +
                   $"{nameof(FileVersion)}: {FileVersion}, " +
                   $"{nameof(DataOffset)}: {DataOffset}, " +
                   $"{nameof(Endianness)}: {Endianness}, " +
                   $"{nameof(Version)}: {Version}" +
                   ")";
        }
    }
}
