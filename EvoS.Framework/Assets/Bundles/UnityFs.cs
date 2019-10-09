using System;
using System.Collections.Generic;
using System.IO;
using K4os.Compression.LZ4;

namespace EvoS.Framework.Assets.Bundles
{
    public class UnityFs
    {
        private StreamReader _stream;
        private AssetLoader _loader;
        public string Name { get; set; }
        public UnityFsHeader Header { get; set; }
        public byte[] Guid { get; set; }
        public List<ArchiveBlockInfo> Blocks = new List<ArchiveBlockInfo>();
        public List<ArchiveNodeInfo> Nodes = new List<ArchiveNodeInfo>();
        public long DataOffset { get; set; }
        public string FilePath { get; set; }

        public UnityFs(AssetLoader loader, string filePath)
        {
            _loader = loader;
            _stream = new StreamReader(filePath);

            FilePath = filePath;
            Header = new UnityFsHeader(_stream);
            var data = _stream.ReadBytes((int) Header.CiBlockSize);
            var outData = new byte[(int) Header.UiBlockSize];

            switch (Header.Compression)
            {
                case CompressionType.None:
                    outData = data;
                    break;
                case CompressionType.Lz4:
                case CompressionType.Lz4Hc:
                    LZ4Codec.Decode(data, 0, data.Length,
                        outData, 0, outData.Length);
                    break;
                default:
                    throw new NotImplementedException();
            }

            var blk = new StreamReader(outData) {ByteOrder = ByteOrder.BigEndian};
            Guid = blk.ReadBytes(16);

            var numBlocks = blk.ReadInt32();
            for (var i = 0; i < numBlocks; i++)
            {
                Blocks.Add(new ArchiveBlockInfo(blk));
            }

            var numNodes = blk.ReadInt32();
            for (var i = 0; i < numNodes; i++)
            {
                Nodes.Add(new ArchiveNodeInfo(blk));
            }

            Name = Nodes[0].Name.ToLower();
            if (Name.EndsWith(".sharedassets"))
            {
                Name = Name.Substring(0, Name.Length - 13);
            }

            DataOffset = _stream.Position;
        }

        public AssetFile OpenAssetFile(ArchiveNodeInfo node)
        {
            if (Blocks.Count != 1 || Blocks[0].Compression != CompressionType.None)
            {
                throw new NotImplementedException();
            }

            var stream = new StreamReader(FilePath, DataOffset + node.Offset);
            return new AssetFile(_loader, node.Name, stream);
        }

        public override string ToString()
        {
            return $"{nameof(UnityFs)}(" +
                   $"{nameof(Name)}: {Name}, " +
                   $"{nameof(Header)}: {Header}, " +
                   $"{nameof(Guid)}: {Guid}, " +
                   $"{nameof(Blocks)}: [{string.Join(", ", Blocks)}], " +
                   $"{nameof(Nodes)}: [{string.Join(", ", Nodes)}]" +
                   ")";
        }
    }

    public struct ArchiveBlockInfo
    {
        public int UncompressedSize;
        public int CompressedSize;
        public short Flags;

        public CompressionType Compression => (CompressionType) (Flags & 0x3F);

        public ArchiveBlockInfo(StreamReader stream)
        {
            UncompressedSize = stream.ReadInt32();
            CompressedSize = stream.ReadInt32();
            Flags = stream.ReadInt16();
        }

        public override string ToString()
        {
            return $"{nameof(ArchiveBlockInfo)}(" +
                   $"{nameof(UncompressedSize)}: {UncompressedSize}, " +
                   $"{nameof(CompressedSize)}: {CompressedSize}, " +
                   $"{nameof(Flags)}: {Flags}, " +
                   $"{nameof(Compression)}: {Compression}" +
                   ")";
        }
    }

    public struct ArchiveNodeInfo
    {
        public long Offset;
        public long Size;
        public int Status;
        public string Name;

        public ArchiveNodeInfo(StreamReader stream)
        {
            Offset = stream.ReadInt64();
            Size = stream.ReadInt64();
            Status = stream.ReadInt32();
            Name = stream.ReadNullString();
        }

        public override string ToString()
        {
            return $"{nameof(ArchiveNodeInfo)}(" +
                   $"{nameof(Offset)}: {Offset}, " +
                   $"{nameof(Size)}: {Size}, " +
                   $"{nameof(Status)}: {Status}, " +
                   $"{nameof(Name)}: {Name}" +
                   ")";
        }
    }
}
