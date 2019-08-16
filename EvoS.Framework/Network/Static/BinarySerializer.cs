using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EvoS.Framework.Network.Static
{
    public class BinarySerializer
    {
        public int ReadVarIntsss(Stream buffer)
        {
            int shift = 0;
            int result = 0;
            int byteValue;

            while (true)
            {
                byteValue = buffer.ReadByte();

                //Console.WriteLine("read value: " + byteValue);

                if (byteValue == -1) break;

                result |= (byteValue & 0x7f) << shift;
                shift += 7;

                if ((byteValue & 0x80) != 0x80) break;
            }

            return result;
        }

        public void WriteVarIntssss(Stream buffer, int value)
        {
            int byteValue;

            while (true)
            {
                byteValue = value & 0x7f;
                value >>= 7;

                if (value != 0)
                {
                    byteValue |= 0x80;
                }
                //Console.WriteLine("writing byte: " + byteValue);
                buffer.WriteByte((byte)byteValue);

                if (value == 0) break;
            }
        }

        public String ReadStringssss(Stream s)
        {
            int length = ReadVarIntsss(s);
            byte[] buffer;

            if (length > 0)
            {
                buffer = new byte[length];
                //s.Read(buffer, 0, length);

                return buffer.ToString();
            }
            else
            {
                return "";
            }
        }

        public void WriteStringsss(Stream s, String str)
        {
            WriteVarIntssss(s, str.Length);
            for (int i = 0; i < str.Length; i++)
            {
                s.WriteByte((byte)str[i]);
            }
        }

    }
}
