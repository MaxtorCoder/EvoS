using System;

namespace EvoS.Framework.Assets.Bundles
{
    public class UnityFsHeader
    {
        public string Signature { get; set; }
        public int FormatVersion { get; set; }
        public string UnityVersion { get; set; }
        public string GeneratorVersion { get; set; }
        public long FileSize { get; set; }
        public uint CiBlockSize { get; set; }
        public uint UiBlockSize { get; set; }
        public uint Flags { get; set; }

        public CompressionType Compression => (CompressionType) (Flags & 0x3F);

        public UnityFsHeader(StreamReader stream)
        {
            Signature = stream.ReadNullString(8);
            if (Signature != "UnityFS")
            {
                throw new ArgumentException("Signature of asset bundle != UnityFS");
            }

            var originalEndianness = stream.ByteOrder;
            stream.ByteOrder = ByteOrder.BigEndian;

            FormatVersion = stream.ReadInt32();
            UnityVersion = stream.ReadNullString();
            GeneratorVersion = stream.ReadNullString();
            FileSize = stream.ReadInt64();
            CiBlockSize = stream.ReadUInt32();
            UiBlockSize = stream.ReadUInt32();
            Flags = stream.ReadUInt32();

            if ((Flags & 0x80) != 0)
            {
                throw new NotImplementedException();
            }
            
            stream.ByteOrder = originalEndianness;
        }


        public override string ToString()
        {
            return $"{nameof(UnityFsHeader)}(" +
                   $"{nameof(Signature)}: {Signature}, " +
                   $"{nameof(FormatVersion)}: {FormatVersion}, " +
                   $"{nameof(UnityVersion)}: {UnityVersion}, " +
                   $"{nameof(GeneratorVersion)}: {GeneratorVersion}, " +
                   $"{nameof(FileSize)}: {FileSize}, " +
                   $"{nameof(CiBlockSize)}: {CiBlockSize}, " +
                   $"{nameof(UiBlockSize)}: {UiBlockSize}, " +
                   $"{nameof(Flags)}: {Flags}, " +
                   $"{nameof(Compression)}: {Compression}" +
                   ")";
        }
    }
}
