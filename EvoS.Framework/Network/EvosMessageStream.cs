using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EvoS.Framework.Network
{
    public class EvosMessageStream
    {
        public MemoryStream stream;

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
            int length = ReadVarInt();
            byte[] buffer;

            if (length > 0){
                buffer = new byte[length];
                return buffer.ToString();
            } else {
                return "";
            }
        }
    }
}
