using System;
using EvoS.Framework.Logging;

namespace EvoS.Framework.Network.Unity
{
    internal class NetBuffer
    {
        private byte[] m_Buffer;
        private uint m_Pos;
        private const int k_InitialSize = 64;
        private const float k_GrowthFactor = 1.5f;
        private const int k_BufferSizeWarning = 134217728;

        public NetBuffer()
        {
            m_Buffer = new byte[64];
        }

        public NetBuffer(byte[] buffer)
        {
            m_Buffer = buffer;
        }

        public uint Position => m_Pos;

        public int Length => m_Buffer.Length;

        public byte ReadByte()
        {
            if (m_Pos >= m_Buffer.Length)
                throw new IndexOutOfRangeException("NetworkReader:ReadByte out of range:" + ToString());
            return m_Buffer[m_Pos++];
        }

        public void ReadBytes(byte[] buffer, uint count)
        {
            if (m_Pos + count > m_Buffer.Length)
                throw new IndexOutOfRangeException("NetworkReader:ReadBytes out of range: (" + count + ") " +
                                                   ToString());
            for (ushort index = 0; (uint) index < count; ++index)
                buffer[index] = m_Buffer[(m_Pos + index)];
            m_Pos += count;
        }

        internal ArraySegment<byte> AsArraySegment()
        {
            return new ArraySegment<byte>(m_Buffer, 0, (int) m_Pos);
        }

        public void WriteByte(byte value)
        {
            WriteCheckForSpace(1);
            m_Buffer[m_Pos] = value;
            ++m_Pos;
        }

        public void WriteByte2(byte value0, byte value1)
        {
            WriteCheckForSpace(2);
            m_Buffer[m_Pos] = value0;
            m_Buffer[(m_Pos + 1U)] = value1;
            m_Pos += 2U;
        }

        public void WriteByte4(byte value0, byte value1, byte value2, byte value3)
        {
            WriteCheckForSpace(4);
            m_Buffer[m_Pos] = value0;
            m_Buffer[(m_Pos + 1U)] = value1;
            m_Buffer[(m_Pos + 2U)] = value2;
            m_Buffer[(m_Pos + 3U)] = value3;
            m_Pos += 4U;
        }

        public void WriteByte8(
            byte value0,
            byte value1,
            byte value2,
            byte value3,
            byte value4,
            byte value5,
            byte value6,
            byte value7)
        {
            WriteCheckForSpace(8);
            m_Buffer[m_Pos] = value0;
            m_Buffer[(m_Pos + 1U)] = value1;
            m_Buffer[(m_Pos + 2U)] = value2;
            m_Buffer[(m_Pos + 3U)] = value3;
            m_Buffer[(m_Pos + 4U)] = value4;
            m_Buffer[(m_Pos + 5U)] = value5;
            m_Buffer[(m_Pos + 6U)] = value6;
            m_Buffer[(m_Pos + 7U)] = value7;
            m_Pos += 8U;
        }

        public void WriteBytesAtOffset(byte[] buffer, ushort targetOffset, ushort count)
        {
            uint num = count + (uint) targetOffset;
            WriteCheckForSpace((ushort) num);
            if (targetOffset == 0 && count == buffer.Length)
            {
                buffer.CopyTo(m_Buffer, (int) m_Pos);
            }
            else
            {
                for (int index = 0; index < (int) count; ++index)
                    m_Buffer[targetOffset + index] = buffer[index];
            }

            if (num <= m_Pos)
                return;
            m_Pos = num;
        }

        public void WriteBytes(byte[] buffer, ushort count)
        {
            WriteCheckForSpace(count);
            if (count == buffer.Length)
            {
                buffer.CopyTo(m_Buffer, (int) m_Pos);
            }
            else
            {
                for (int index = 0; index < (int) count; ++index)
                    m_Buffer[m_Pos + index] = buffer[index];
            }

            m_Pos += count;
        }

        private void WriteCheckForSpace(ushort count)
        {
            if (m_Pos + count < m_Buffer.Length)
                return;
            int length = (int) Math.Ceiling(m_Buffer.Length * 1.5);
            while (m_Pos + count >= length)
            {
                length = (int) Math.Ceiling(length * 1.5);
                if (length > 134217728)
                    Log.Print(LogType.Warning, $"NetworkBuffer size is {length} bytes!");
            }

            byte[] numArray = new byte[length];
            m_Buffer.CopyTo(numArray, 0);
            m_Buffer = numArray;
        }

        public void FinishMessage()
        {
            ushort num = (ushort) (m_Pos - 4U);
            m_Buffer[0] = (byte) (num & (uint) byte.MaxValue);
            m_Buffer[1] = (byte) (num >> 8 & byte.MaxValue);
            WriteCheckForSpace(4);
            for (int index = (int) m_Pos - 1; index >= 0; --index)
                m_Buffer[index + 4] = m_Buffer[index];
            m_Pos += 4U;
        }

        public void WriteSeqNum(uint seqNum)
        {
            m_Buffer[0] = (byte) (seqNum & byte.MaxValue);
            m_Buffer[1] = (byte) (seqNum >> 8 & byte.MaxValue);
            m_Buffer[2] = (byte) (seqNum >> 16 & byte.MaxValue);
            m_Buffer[3] = (byte) (seqNum >> 24 & byte.MaxValue);
        }

        public void SeekZero()
        {
            m_Pos = 0U;
        }

        public void Replace(byte[] buffer)
        {
            m_Buffer = buffer;
            m_Pos = 0U;
        }

        public override string ToString()
        {
            return $"NetBuf sz:{m_Buffer.Length} pos:{m_Pos}";
        }
    }
}
