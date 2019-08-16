using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EvoS.Framework.Network
{
    public class EvosMessageStream
    {
        private MemoryStream stream;
        private MemoryStream outputStream;

        public EvosMessageStream(MemoryStream ms)
        {
            stream = ms;
            stream.Seek(0, SeekOrigin.Begin);

            outputStream = new MemoryStream();
        }

        public MemoryStream GetOutputStream()
        {
            return outputStream;
        }

        public bool ReadBool()
        {
            return stream.ReadByte() != 0;
        }

        public long ReadLong()
        {
            long n = (long)ReadVarInt();
            return (long)(n >> 1 ^ -(n & 1L));
        }

        public int ReadVarInt()
        {
            int shift = 0;
            int result = 0;
            int byteValue;

            while (true)
            {
                byteValue = stream.ReadByte();
                //Console.WriteLine("read value: " + byteValue);
                if (byteValue == -1) break;
                result |= ((byteValue & 0x7f) << shift);
                shift += 7;
                if ((byteValue & 0x80) != 0x80) break;
            }

            return result;
        }

        public String ReadString()
        {
            int data_length = ReadVarInt();

            byte[] buffer;

            if (data_length == 0)
            {
                return null;
            }
            else if (data_length == 1)
            {
                return string.Empty;
            }
            else
            {
                int string_length = ReadVarInt();
                buffer = new byte[string_length];
                stream.Read(buffer, 0, string_length);
                return Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            }
        }

        /*
         * Writes a number an returns the amount of bytes written
         */
        public int WriteVarInt(int value)
        {
            int byteValue;
            int byteCount = 0;

            while (true)
            {
                byteValue = value & 0x7f;
                value >>= 7;

                if (value != 0)
                {
                    byteValue |= 0x80;
                }
                //Console.WriteLine("writing byte: " + byteValue);
                outputStream.WriteByte((byte)byteValue);
                byteCount++;

                if (value == 0) return byteCount;
            }
        }

        public int WriteString(String str)
        {
            int byteCount = 0;

            if (str == null)
            {
                return WriteVarInt(0);
            }
            if (str.Length == 0)
            {
                return WriteVarInt(1);
            }

            byteCount += WriteVarInt(str.Length + 1);
            byteCount += WriteVarInt(str.Length);
            byte[] buffer = Encoding.GetEncoding("UTF-8").GetBytes(str.ToCharArray());
            outputStream.Write(buffer, 0, buffer.Length);
            byteCount += buffer.Length;

            return byteCount;
        }

        public int WriteBool(bool value)
        {
            outputStream.WriteByte(value ? ((byte)1) : ((byte)0));
            return 1;
        }
    }
}
