using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using EvoS.Framework.Misc;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Assets
{
    public enum ByteOrder : int
    {
        LittleEndian,
        BigEndian
    }

    public class StreamReader : IDisposable
    {
        private Stream _stream;
        private const int MaxStringLength = 32768;
        private const int InitialStringBufferSize = 1024;
        private static byte[] _stringReaderBuffer;
        private static Encoding _encoding;
        public ByteOrder ByteOrder = ByteOrder.LittleEndian;
        public long DataOffset { get; private set; }

        public StreamReader(Stream stream, long dataOffset = 0)
        {
            _stream = stream;
            DataOffset = dataOffset;
            _stream.Position = dataOffset;
            Initialize();
        }

        public StreamReader(byte[] data, long dataOffset = 0) : this(new MemoryStream(data), dataOffset)
        {
        }

        public StreamReader(string filePath, long dataOffset = 0) : this(File.OpenRead(filePath), dataOffset)
        {
        }

        private static void Initialize()
        {
            if (_encoding != null)
                return;
            _stringReaderBuffer = new byte[InitialStringBufferSize];
            _encoding = new UTF8Encoding();
        }

        public long Position
        {
            get => _stream.Position - DataOffset;
            set
            {
//                var old = _stream.Position;
                _stream.Position = DataOffset + value;
//                Console.WriteLine($"Seeked from {old} to {value}");
            }
        }

        public long Length => _stream.Length;

        public void AlignTo(int offset = 4)
        {
            if (Position % offset == 0) return;

            Position += (offset - Position % offset) % offset;
        }

        public void SeekZero()
        {
            _stream.Position = 0;
        }

        public byte ReadByte()
        {
            var data = _stream.ReadByte();
            if (data == -1)
            {
                throw new EndOfStreamException();
            }

            return (byte) data;
        }

        public sbyte ReadSByte()
        {
            return (sbyte) ReadByte();
        }

        public short ReadInt16()
        {
            if (ByteOrder == ByteOrder.LittleEndian)
            {
                return (short) (ushort) ((ushort) (0U | _stream.ReadByte()) |
                                         (uint) (ushort) ((uint) ReadByte() << 8));
            }
            else
            {
                return (short) (ushort) ((uint) (ushort) ((uint) ReadByte() << 8) |
                                         (ushort) (0U | _stream.ReadByte()));
            }
        }

        public ushort ReadUInt16()
        {
            if (ByteOrder == ByteOrder.LittleEndian)
            {
                return (ushort) ((ushort) (0U | _stream.ReadByte()) |
                                 (uint) (ushort) ((uint) ReadByte() << 8));
            }
            else
            {
                return (ushort) ((ushort) ((uint) ReadByte() << 8) |
                                 (uint) (ushort) (0U | _stream.ReadByte()));
            }
        }

        public int ReadInt32()
        {
            if (ByteOrder == ByteOrder.LittleEndian)
            {
                return (int) (0U | ReadByte() | (uint) ReadByte() << 8 |
                              (uint) ReadByte() << 16 | (uint) ReadByte() << 24);
            }
            else
            {
                return (int) (0U | (uint) ReadByte() << 24 | (uint) ReadByte() << 16 |
                              (uint) ReadByte() << 8 | ReadByte());
            }
        }

        public uint ReadUInt32()
        {
            if (ByteOrder == ByteOrder.LittleEndian)
            {
                return 0U | ReadByte() | (uint) ReadByte() << 8 |
                       (uint) ReadByte() << 16 | (uint) ReadByte() << 24;
            }
            else
            {
                return 0U | (uint) ReadByte() << 24 | (uint) ReadByte() << 16 |
                       (uint) ReadByte() << 8 | ReadByte();
            }
        }

        public long ReadInt64()
        {
            if (ByteOrder == ByteOrder.LittleEndian)
            {
                return (long) (0UL | ReadByte() | (ulong) ReadByte() << 8 |
                               (ulong) ReadByte() << 16 | (ulong) ReadByte() << 24 |
                               (ulong) ReadByte() << 32 | (ulong) ReadByte() << 40 |
                               (ulong) ReadByte() << 48 | (ulong) ReadByte() << 56);
            }
            else
            {
                return (long) (0UL | (ulong) ReadByte() << 56 | (ulong) ReadByte() << 48 |
                               (ulong) ReadByte() << 40 | (ulong) ReadByte() << 32 |
                               (ulong) ReadByte() << 24 | (ulong) ReadByte() << 16 |
                               (ulong) ReadByte() << 8 | ReadByte());
            }
        }

        public ulong ReadUInt64()
        {
            if (ByteOrder == ByteOrder.LittleEndian)
            {
                return 0UL | ReadByte() | (ulong) ReadByte() << 8 |
                       (ulong) ReadByte() << 16 | (ulong) ReadByte() << 24 |
                       (ulong) ReadByte() << 32 | (ulong) ReadByte() << 40 |
                       (ulong) ReadByte() << 48 | (ulong) ReadByte() << 56;
            }
            else
            {
                return 0UL | (ulong) ReadByte() << 56 | (ulong) ReadByte() << 48 |
                       (ulong) ReadByte() << 40 | (ulong) ReadByte() << 32 |
                       (ulong) ReadByte() << 24 | (ulong) ReadByte() << 16 |
                       (ulong) ReadByte() << 8 | ReadByte();
            }
        }

        public Decimal ReadDecimal()
        {
            var bits = new int[4]
            {
                ReadInt32(),
                ReadInt32(),
                ReadInt32(),
                ReadInt32()
            };

            if (ByteOrder == ByteOrder.LittleEndian)
            {
                return new Decimal(bits);
            }
            else
            {
                return new Decimal(bits.Reverse().ToArray());
            }
        }

        public float ReadSingle()
        {
            return FloatConversion.ToSingle(ReadUInt32());
        }

        public double ReadDouble()
        {
            return FloatConversion.ToDouble(ReadUInt64());
        }

        public string ReadString()
        {
            ushort num = ReadUInt16();
            if (num == 0)
                return "";
            if (num >= MaxStringLength)
                throw new IndexOutOfRangeException("ReadString() too long: " + num);
            while (num > _stringReaderBuffer.Length)
                _stringReaderBuffer = new byte[_stringReaderBuffer.Length * 2];
            _stream.Read(_stringReaderBuffer, 0, num);
            AlignTo();

            return new string(_encoding.GetChars(_stringReaderBuffer, 0, num));
        }

        public string ReadString32()
        {
            var num = ReadInt32();
            if (num == 0)
                return "";
            if (num >= MaxStringLength)
                throw new IndexOutOfRangeException("ReadString() too long: " + num);
            while (num > _stringReaderBuffer.Length)
                _stringReaderBuffer = new byte[_stringReaderBuffer.Length * 2];
            _stream.Read(_stringReaderBuffer, 0, num);
            AlignTo();

            return new string(_encoding.GetChars(_stringReaderBuffer, 0, num));
        }

        public string ReadNullString(int maxLength = -1)
        {
            var size = 0;
            // we'll lazily reuse the string buffer, null terminated strings shouldn't be too long
            while (size < _stringReaderBuffer.Length)
            {
                if (size == maxLength)
                {
                    break;
                }
                
                var data = ReadByte();
                if (data == 0)
                {
                    break;
                }

                _stringReaderBuffer[size++] = data;
            }

            return new string(_encoding.GetChars(_stringReaderBuffer, 0, size));
        }

        public char ReadChar()
        {
            return (char) ReadByte();
        }

        public bool ReadBoolean()
        {
            return ReadByte() == 1;
        }

        public byte[] ReadBytes(int count)
        {
            if (count < 0)
                throw new IndexOutOfRangeException("StreamReader ReadBytes " + count);
            byte[] buffer = new byte[count];
            _stream.Read(buffer, 0, count);
            return buffer;
        }

        public byte[] ReadBytesAndSize()
        {
            ushort num = ReadUInt16();
            if (num == 0)
                return null;
            return ReadBytes(num);
        }

        public Vector2 ReadVector2()
        {
            return new Vector2(ReadSingle(), ReadSingle());
        }

        public Vector3 ReadVector3()
        {
            return new Vector3(ReadSingle(), ReadSingle(), ReadSingle());
        }

        public Quaternion ReadQuaternion()
        {
            return new Quaternion(ReadSingle(), ReadSingle(), ReadSingle(), ReadSingle());
        }

        public NetworkHash128 ReadNetworkHash128()
        {
            NetworkHash128 networkHash128;
            networkHash128.i0 = ReadByte();
            networkHash128.i1 = ReadByte();
            networkHash128.i2 = ReadByte();
            networkHash128.i3 = ReadByte();
            networkHash128.i4 = ReadByte();
            networkHash128.i5 = ReadByte();
            networkHash128.i6 = ReadByte();
            networkHash128.i7 = ReadByte();
            networkHash128.i8 = ReadByte();
            networkHash128.i9 = ReadByte();
            networkHash128.i10 = ReadByte();
            networkHash128.i11 = ReadByte();
            networkHash128.i12 = ReadByte();
            networkHash128.i13 = ReadByte();
            networkHash128.i14 = ReadByte();
            networkHash128.i15 = ReadByte();
            return networkHash128;
        }

        public override string ToString()
        {
            return _stream.ToString();
        }

        public void Dispose()
        {
            _stream?.Dispose();
        }

        public ColorRGBA ReadColorRGBA()
        {
            return new ColorRGBA(
                ReadSingle(),
                ReadSingle(),
                ReadSingle(),
                ReadSingle()
            );
        }
    }
}
