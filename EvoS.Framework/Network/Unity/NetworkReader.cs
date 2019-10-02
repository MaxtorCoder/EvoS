using System;
using System.Numerics;
using System.Text;

namespace EvoS.Framework.Network.Unity
{
    public class NetworkReader
    {
        private NetBuffer m_buf;
        private const int k_MaxStringLength = 32768;
        private const int k_InitialStringBufferSize = 1024;
        private static byte[] s_StringReaderBuffer;
        private static Encoding s_Encoding;

        public NetworkReader()
        {
            m_buf = new NetBuffer();
            Initialize();
        }

        public NetworkReader(NetworkWriter writer)
        {
            m_buf = new NetBuffer(writer.AsArray());
            Initialize();
        }

        public NetworkReader(byte[] buffer)
        {
            m_buf = new NetBuffer(buffer);
            Initialize();
        }

        private static void Initialize()
        {
            if (s_Encoding != null)
                return;
            s_StringReaderBuffer = new byte[1024];
            s_Encoding = new UTF8Encoding();
        }

        public uint Position
        {
            get { return m_buf.Position; }
        }

        public int Length
        {
            get { return m_buf.Length; }
        }

        public void SeekZero()
        {
            m_buf.SeekZero();
        }

        internal void Replace(byte[] buffer)
        {
            m_buf.Replace(buffer);
        }

        public uint ReadPackedUInt32()
        {
            byte num1 = ReadByte();
            if (num1 < 241)
                return num1;
            byte num2 = ReadByte();
            if (num1 >= 241 && num1 <= 248)
                return (uint) (240 + 256 * (num1 - 241)) + num2;
            byte num3 = ReadByte();
            if (num1 == 249)
                return (uint) (2288 + 256 * num2) + num3;
            byte num4 = ReadByte();
            if (num1 == 250)
                return (uint) (num2 + (num3 << 8) + (num4 << 16));
            byte num5 = ReadByte();
            if (num1 >= 251)
                return (uint) (num2 + (num3 << 8) + (num4 << 16) + (num5 << 24));
            throw new IndexOutOfRangeException("ReadPackedUInt32() failure: " + num1);
        }

        public ulong ReadPackedUInt64()
        {
            byte num1 = ReadByte();
            if (num1 < 241)
                return num1;
            byte num2 = ReadByte();
            if (num1 >= 241 && num1 <= 248)
                return (ulong) (240L + 256L * (num1 - 241L)) + num2;
            byte num3 = ReadByte();
            if (num1 == 249)
                return (ulong) (2288L + 256L * num2) + num3;
            byte num4 = ReadByte();
            if (num1 == 250)
                return (ulong) (num2 + ((long) num3 << 8) + ((long) num4 << 16));
            byte num5 = ReadByte();
            if (num1 == 251)
                return (ulong) (num2 + ((long) num3 << 8) + ((long) num4 << 16) + ((long) num5 << 24));
            byte num6 = ReadByte();
            if (num1 == 252)
                return (ulong) (num2 + ((long) num3 << 8) + ((long) num4 << 16) + ((long) num5 << 24) +
                                ((long) num6 << 32));
            byte num7 = ReadByte();
            if (num1 == 253)
                return (ulong) (num2 + ((long) num3 << 8) + ((long) num4 << 16) + ((long) num5 << 24) +
                                ((long) num6 << 32) + ((long) num7 << 40));
            byte num8 = ReadByte();
            if (num1 == 254)
                return (ulong) (num2 + ((long) num3 << 8) + ((long) num4 << 16) + ((long) num5 << 24) +
                                ((long) num6 << 32) + ((long) num7 << 40) + ((long) num8 << 48));
            byte num9 = ReadByte();
            if (num1 == byte.MaxValue)
                return (ulong) (num2 + ((long) num3 << 8) + ((long) num4 << 16) + ((long) num5 << 24) +
                                ((long) num6 << 32) + ((long) num7 << 40) + ((long) num8 << 48) + ((long) num9 << 56));
            throw new IndexOutOfRangeException("ReadPackedUInt64() failure: " + num1);
        }

        public NetworkInstanceId ReadNetworkId()
        {
            return new NetworkInstanceId(ReadPackedUInt32());
        }

        public NetworkSceneId ReadSceneId()
        {
            return new NetworkSceneId(ReadPackedUInt32());
        }

        public byte ReadByte()
        {
            return m_buf.ReadByte();
        }

        public sbyte ReadSByte()
        {
            return (sbyte) m_buf.ReadByte();
        }

        public short ReadInt16()
        {
            return (short) (ushort) ((ushort) (0U | m_buf.ReadByte()) |
                                     (uint) (ushort) ((uint) m_buf.ReadByte() << 8));
        }

        public ushort ReadUInt16()
        {
            return (ushort) ((ushort) (0U | m_buf.ReadByte()) |
                             (uint) (ushort) ((uint) m_buf.ReadByte() << 8));
        }

        public int ReadInt32()
        {
            return (int) (0U | m_buf.ReadByte() | (uint) m_buf.ReadByte() << 8 |
                          (uint) m_buf.ReadByte() << 16 | (uint) m_buf.ReadByte() << 24);
        }

        public uint ReadUInt32()
        {
            return 0U | m_buf.ReadByte() | (uint) m_buf.ReadByte() << 8 |
                   (uint) m_buf.ReadByte() << 16 | (uint) m_buf.ReadByte() << 24;
        }

        public long ReadInt64()
        {
            return (long) (0UL | m_buf.ReadByte() | (ulong) m_buf.ReadByte() << 8 |
                           (ulong) m_buf.ReadByte() << 16 | (ulong) m_buf.ReadByte() << 24 |
                           (ulong) m_buf.ReadByte() << 32 | (ulong) m_buf.ReadByte() << 40 |
                           (ulong) m_buf.ReadByte() << 48 | (ulong) m_buf.ReadByte() << 56);
        }

        public ulong ReadUInt64()
        {
            return 0UL | m_buf.ReadByte() | (ulong) m_buf.ReadByte() << 8 |
                   (ulong) m_buf.ReadByte() << 16 | (ulong) m_buf.ReadByte() << 24 |
                   (ulong) m_buf.ReadByte() << 32 | (ulong) m_buf.ReadByte() << 40 |
                   (ulong) m_buf.ReadByte() << 48 | (ulong) m_buf.ReadByte() << 56;
        }

        public Decimal ReadDecimal()
        {
            return new Decimal(new int[4]
            {
                ReadInt32(),
                ReadInt32(),
                ReadInt32(),
                ReadInt32()
            });
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
            if (num >= 32768)
                throw new IndexOutOfRangeException("ReadString() too long: " + num);
            while (num > s_StringReaderBuffer.Length)
                s_StringReaderBuffer = new byte[s_StringReaderBuffer.Length * 2];
            m_buf.ReadBytes(s_StringReaderBuffer, num);
            return new string(s_Encoding.GetChars(s_StringReaderBuffer, 0, num));
        }

        public char ReadChar()
        {
            return (char) m_buf.ReadByte();
        }

        public bool ReadBoolean()
        {
            return m_buf.ReadByte() == 1;
        }

        public byte[] ReadBytes(int count)
        {
            if (count < 0)
                throw new IndexOutOfRangeException("NetworkReader ReadBytes " + count);
            byte[] buffer = new byte[count];
            m_buf.ReadBytes(buffer, (uint) count);
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

        public Vector4 ReadVector4()
        {
            return new Vector4(ReadSingle(), ReadSingle(), ReadSingle(), ReadSingle());
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
            return m_buf.ToString();
        }

        public TMsg ReadMessage<TMsg>() where TMsg : MessageBase, new()
        {
            TMsg msg = new TMsg();
            msg.Deserialize(this);
            return msg;
        }
    }
}
