using System;
using System.Drawing;
using System.Numerics;
using System.Text;
using EvoS.Framework.Logging;

namespace EvoS.Framework.Network.Unity
{
    public class NetworkWriter
    {
        private const int k_MaxStringLength = 32768;
        private NetBuffer m_Buffer;
        private static Encoding s_Encoding;
        private static byte[] s_StringWriteBuffer;
        private static UIntFloat s_FloatConverter;

        public NetworkWriter()
        {
            m_Buffer = new NetBuffer();
            if (s_Encoding != null)
                return;
            s_Encoding = new UTF8Encoding();
            s_StringWriteBuffer = new byte[32768];
        }

        public NetworkWriter(byte[] buffer)
        {
            m_Buffer = new NetBuffer(buffer);
            if (s_Encoding != null)
                return;
            s_Encoding = new UTF8Encoding();
            s_StringWriteBuffer = new byte[32768];
        }

        public short Position => (short) m_Buffer.Position;

        public byte[] ToArray()
        {
            byte[] numArray = new byte[m_Buffer.AsArraySegment().Count];
            Array.Copy(m_Buffer.AsArraySegment().Array, numArray,
                m_Buffer.AsArraySegment().Count);
            return numArray;
        }

        public byte[] AsArray()
        {
            return AsArraySegment().Array;
        }

        public ArraySegment<byte> AsArraySegment()
        {
            return m_Buffer.AsArraySegment();
        }

        public void WritePackedUInt32(uint value)
        {
            if (value <= 240U)
                Write((byte) value);
            else if (value <= 2287U)
            {
                Write((byte) ((value - 240U) / 256U + 241U));
                Write((byte) ((value - 240U) % 256U));
            }
            else if (value <= 67823U)
            {
                Write((byte) 249);
                Write((byte) ((value - 2288U) / 256U));
                Write((byte) ((value - 2288U) % 256U));
            }
            else if (value <= 16777215U)
            {
                Write((byte) 250);
                Write((byte) (value & byte.MaxValue));
                Write((byte) (value >> 8 & byte.MaxValue));
                Write((byte) (value >> 16 & byte.MaxValue));
            }
            else
            {
                Write((byte) 251);
                Write((byte) (value & byte.MaxValue));
                Write((byte) (value >> 8 & byte.MaxValue));
                Write((byte) (value >> 16 & byte.MaxValue));
                Write((byte) (value >> 24 & byte.MaxValue));
            }
        }

        public void WritePackedUInt64(ulong value)
        {
            if (value <= 240UL)
                Write((byte) value);
            else if (value <= 2287UL)
            {
                Write((byte) ((value - 240UL) / 256UL + 241UL));
                Write((byte) ((value - 240UL) % 256UL));
            }
            else if (value <= 67823UL)
            {
                Write((byte) 249);
                Write((byte) ((value - 2288UL) / 256UL));
                Write((byte) ((value - 2288UL) % 256UL));
            }
            else if (value <= 16777215UL)
            {
                Write((byte) 250);
                Write((byte) (value & byte.MaxValue));
                Write((byte) (value >> 8 & byte.MaxValue));
                Write((byte) (value >> 16 & byte.MaxValue));
            }
            else if (value <= uint.MaxValue)
            {
                Write((byte) 251);
                Write((byte) (value & byte.MaxValue));
                Write((byte) (value >> 8 & byte.MaxValue));
                Write((byte) (value >> 16 & byte.MaxValue));
                Write((byte) (value >> 24 & byte.MaxValue));
            }
            else if (value <= 1099511627775UL)
            {
                Write((byte) 252);
                Write((byte) (value & byte.MaxValue));
                Write((byte) (value >> 8 & byte.MaxValue));
                Write((byte) (value >> 16 & byte.MaxValue));
                Write((byte) (value >> 24 & byte.MaxValue));
                Write((byte) (value >> 32 & byte.MaxValue));
            }
            else if (value <= 281474976710655UL)
            {
                Write((byte) 253);
                Write((byte) (value & byte.MaxValue));
                Write((byte) (value >> 8 & byte.MaxValue));
                Write((byte) (value >> 16 & byte.MaxValue));
                Write((byte) (value >> 24 & byte.MaxValue));
                Write((byte) (value >> 32 & byte.MaxValue));
                Write((byte) (value >> 40 & byte.MaxValue));
            }
            else if (value <= 72057594037927935UL)
            {
                Write((byte) 254);
                Write((byte) (value & byte.MaxValue));
                Write((byte) (value >> 8 & byte.MaxValue));
                Write((byte) (value >> 16 & byte.MaxValue));
                Write((byte) (value >> 24 & byte.MaxValue));
                Write((byte) (value >> 32 & byte.MaxValue));
                Write((byte) (value >> 40 & byte.MaxValue));
                Write((byte) (value >> 48 & byte.MaxValue));
            }
            else
            {
                Write(byte.MaxValue);
                Write((byte) (value & byte.MaxValue));
                Write((byte) (value >> 8 & byte.MaxValue));
                Write((byte) (value >> 16 & byte.MaxValue));
                Write((byte) (value >> 24 & byte.MaxValue));
                Write((byte) (value >> 32 & byte.MaxValue));
                Write((byte) (value >> 40 & byte.MaxValue));
                Write((byte) (value >> 48 & byte.MaxValue));
                Write((byte) (value >> 56 & byte.MaxValue));
            }
        }

        public void Write(NetworkInstanceId value)
        {
            WritePackedUInt32(value.Value);
        }

        public void Write(NetworkSceneId value)
        {
            WritePackedUInt32(value.Value);
        }

        public void Write(char value)
        {
            m_Buffer.WriteByte((byte) value);
        }

        public void Write(byte value)
        {
            m_Buffer.WriteByte(value);
        }

        public void Write(sbyte value)
        {
            m_Buffer.WriteByte((byte) value);
        }

        public void Write(short value)
        {
            m_Buffer.WriteByte2((byte) ((uint) value & byte.MaxValue),
                (byte) (value >> 8 & byte.MaxValue));
        }

        public void Write(ushort value)
        {
            m_Buffer.WriteByte2((byte) (value & (uint) byte.MaxValue),
                (byte) (value >> 8 & byte.MaxValue));
        }

        public void Write(int value)
        {
            m_Buffer.WriteByte4((byte) (value & byte.MaxValue), (byte) (value >> 8 & byte.MaxValue),
                (byte) (value >> 16 & byte.MaxValue), (byte) (value >> 24 & byte.MaxValue));
        }

        public void Write(uint value)
        {
            m_Buffer.WriteByte4((byte) (value & byte.MaxValue), (byte) (value >> 8 & byte.MaxValue),
                (byte) (value >> 16 & byte.MaxValue), (byte) (value >> 24 & byte.MaxValue));
        }

        public void Write(long value)
        {
            m_Buffer.WriteByte8((byte) ((ulong) value & byte.MaxValue),
                (byte) ((ulong) (value >> 8) & byte.MaxValue),
                (byte) ((ulong) (value >> 16) & byte.MaxValue),
                (byte) ((ulong) (value >> 24) & byte.MaxValue),
                (byte) ((ulong) (value >> 32) & byte.MaxValue),
                (byte) ((ulong) (value >> 40) & byte.MaxValue),
                (byte) ((ulong) (value >> 48) & byte.MaxValue),
                (byte) ((ulong) (value >> 56) & byte.MaxValue));
        }

        public void Write(ulong value)
        {
            m_Buffer.WriteByte8((byte) (value & byte.MaxValue),
                (byte) (value >> 8 & byte.MaxValue), (byte) (value >> 16 & byte.MaxValue),
                (byte) (value >> 24 & byte.MaxValue), (byte) (value >> 32 & byte.MaxValue),
                (byte) (value >> 40 & byte.MaxValue), (byte) (value >> 48 & byte.MaxValue),
                (byte) (value >> 56 & byte.MaxValue));
        }

        public void Write(float value)
        {
            s_FloatConverter.floatValue = value;
            Write(s_FloatConverter.intValue);
        }

        public void Write(double value)
        {
            s_FloatConverter.doubleValue = value;
            Write(s_FloatConverter.longValue);
        }

        public void Write(Decimal value)
        {
            int[] bits = Decimal.GetBits(value);
            Write(bits[0]);
            Write(bits[1]);
            Write(bits[2]);
            Write(bits[3]);
        }

        public void Write(string value)
        {
            if (value == null)
            {
                m_Buffer.WriteByte2(0, 0);
            }
            else
            {
                int byteCount = s_Encoding.GetByteCount(value);
                if (byteCount >= 32768)
                    throw new IndexOutOfRangeException("Serialize(string) too long: " + value.Length);
                Write((ushort) byteCount);
                int bytes = s_Encoding.GetBytes(value, 0, value.Length, s_StringWriteBuffer,
                    0);
                m_Buffer.WriteBytes(s_StringWriteBuffer, (ushort) bytes);
            }
        }

        public void Write(bool value)
        {
            if (value)
                m_Buffer.WriteByte(1);
            else
                m_Buffer.WriteByte(0);
        }

        public void Write(byte[] buffer, int count)
        {
            if (count > ushort.MaxValue)
            {
                Log.Print(LogType.Error, "NetworkWriter Write: buffer is too large (" + count +
                                         ") bytes. The maximum buffer size is 64K bytes.");
            }
            else
                m_Buffer.WriteBytes(buffer, (ushort) count);
        }

        public void Write(byte[] buffer, int offset, int count)
        {
            if (count > ushort.MaxValue)
            {
                Log.Print(LogType.Error, "NetworkWriter Write: buffer is too large (" + count +
                                         ") bytes. The maximum buffer size is 64K bytes.");
            }
            else
                m_Buffer.WriteBytesAtOffset(buffer, (ushort) offset, (ushort) count);
        }

        public void WriteBytesAndSize(byte[] buffer, int count)
        {
            if (buffer == null || count == 0)
                Write((ushort) 0);
            else if (count > ushort.MaxValue)
            {
                Log.Print(LogType.Error, "NetworkWriter Write: buffer is too large (" + count +
                                         ") bytes. The maximum buffer size is 64K bytes.");
            }
            else
            {
                Write((ushort) count);
                m_Buffer.WriteBytes(buffer, (ushort) count);
            }
        }

        public void WriteBytesFull(byte[] buffer)
        {
            if (buffer == null)
                Write((ushort) 0);
            else if (buffer.Length > ushort.MaxValue)
            {
                Log.Print(LogType.Error, "NetworkWriter Write: buffer is too large (" + buffer.Length +
                                         ") bytes. The maximum buffer size is 64K bytes.");
            }
            else
            {
                Write((ushort) buffer.Length);
                m_Buffer.WriteBytes(buffer, (ushort) buffer.Length);
            }
        }

        public void Write(Vector2 value)
        {
            Write(value.X);
            Write(value.Y);
        }

        public void Write(Vector3 value)
        {
            Write(value.X);
            Write(value.Y);
            Write(value.Z);
        }

        public void Write(Vector4 value)
        {
            Write(value.X);
            Write(value.Y);
            Write(value.Z);
            Write(value.W);
        }

        public void Write(Color value)
        {
            Write(value.R);
            Write(value.G);
            Write(value.B);
            Write(value.A);
        }

        public void Write(Quaternion value)
        {
            Write(value.X);
            Write(value.Y);
            Write(value.Z);
            Write(value.W);
        }

        public void Write(NetworkHash128 value)
        {
            Write(value.i0);
            Write(value.i1);
            Write(value.i2);
            Write(value.i3);
            Write(value.i4);
            Write(value.i5);
            Write(value.i6);
            Write(value.i7);
            Write(value.i8);
            Write(value.i9);
            Write(value.i10);
            Write(value.i11);
            Write(value.i12);
            Write(value.i13);
            Write(value.i14);
            Write(value.i15);
        }

        public void Write(MessageBase msg)
        {
            msg.Serialize(this);
        }

        public void SeekZero()
        {
            m_Buffer.SeekZero();
        }

        public void StartMessage(short msgType)
        {
            SeekZero();
            m_Buffer.WriteByte2(0, 0);
            Write(msgType);
        }

        public void FinishMessage()
        {
            m_Buffer.FinishMessage();
        }

        public void WriteSeqNum(uint seqNum)
        {
            m_Buffer.WriteSeqNum(seqNum);
        }
    }
}
