using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EvoS.Framework.Network
{
    public class EvosMessageStream
    {
        private MemoryStream stream;

        public EvosMessageStream(MemoryStream ms) {
            stream = ms;
            stream.Seek(0, SeekOrigin.Begin);
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
                result |= (byteValue & 0x7f) << shift;
                shift += 7;
                if ((byteValue & 0x80) != 0x80) break;
            }

            return result;
        }

        public String ReadString()
        {
            int data_length = ReadVarInt();
            int string_length = ReadVarInt();
            byte[] buffer;

            if (data_length > 0){
                buffer = new byte[string_length];
                stream.Read(buffer, 0, string_length);
                return Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            } else {
                return "";
            }
        }
    }
}
