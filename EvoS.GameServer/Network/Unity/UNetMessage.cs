using System;
using EvoS.Framework.Logging;

namespace EvoS.GameServer.Network.Unity
{
    public class UNetMessage
    {
        public const int MSG_HEADER_SIZE = 9;
        public byte[] Bytes;
        public int NumBytes;

        public byte[] Serialize()
        {
            if (Bytes == null || NumBytes + 1 < MSG_HEADER_SIZE)
            {
                Log.Print(LogType.Error, $"BinaryMessage.Serialize invalid message numBytes={NumBytes}");
                return null;
            }

            byte[] numArray = new byte[NumBytes + 1];
            numArray[0] = 0;
            Buffer.BlockCopy(Bytes, 0, numArray, 1, NumBytes);
            return numArray;
        }

        public void Deserialize(byte[] rawData)
        {
            if (rawData == null || rawData.Length < MSG_HEADER_SIZE)
            {
                Log.Print(LogType.Error, $"BinaryMessage.Deserialize invalid message bytes {rawData?.Length ?? -1}");
            }
            else
            {
                NumBytes = rawData.Length - 1;
                Bytes = new byte[NumBytes];
                Buffer.BlockCopy(rawData, 1, Bytes, 0, NumBytes);
            }
        }
    }
}
