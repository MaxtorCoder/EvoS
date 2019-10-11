using System.Numerics;

namespace EvoS.Framework.Network.Unity
{
    public class NetworkWriterAdapter : IBitStream
    {
        private NetworkWriter m_stream;

        internal NetworkWriterAdapter(NetworkWriter stream)
        {
            m_stream = stream;
        }

        public bool isReading => false;

        public bool isWriting => true;

        public uint Position
        {
            get
            {
                if (m_stream.Position < 0)
                    return 0;
                return (uint) m_stream.Position;
            }
        }

        public NetworkWriter Writer => m_stream;

        public void Serialize(ref bool value)
        {
            m_stream.Write(value);
        }

        public void Serialize(ref char value)
        {
            m_stream.Write(value);
        }

        public void Serialize(ref byte value)
        {
            m_stream.Write(value);
        }

        public void Serialize(ref sbyte value)
        {
            m_stream.Write(value);
        }

        public void Serialize(ref float value)
        {
            m_stream.Write(value);
        }

        public void Serialize(ref int value)
        {
            m_stream.Write(value);
        }

        public void Serialize(ref Quaternion value)
        {
            m_stream.Write(value);
        }

        public void Serialize(ref short value)
        {
            m_stream.Write(value);
        }

        public void Serialize(ref Vector3 value)
        {
            m_stream.Write(value);
        }

        public void Serialize(ref uint value)
        {
            m_stream.WritePackedUInt32(value);
        }

        public void Serialize(ref long value)
        {
            m_stream.Write(value);
        }

        public void Serialize(ref ulong value)
        {
            m_stream.Write(value);
        }

        public void Serialize(ref string value)
        {
            m_stream.Write(value);
        }

        public void Write(byte[] buffer, int offset, int count)
        {
            m_stream.Write(buffer, offset, count);
        }

        public byte[] ReadBytes(int count)
        {
            return null;
        }
    }
}
