using System;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(784, typeof(LobbyGameClientSystemInfo))]
    public class LobbyGameClientSystemInfo
    {
        public string GraphicsDeviceName;

        public static LobbyGameClientSystemInfo CreateFromStream(EvosMessageStream stream)
        {
            LobbyGameClientSystemInfo ret = new LobbyGameClientSystemInfo();

            int typeId = stream.ReadVarInt();
            ret.GraphicsDeviceName = stream.ReadString();

            Console.WriteLine("GraphicsDeviceName: " + ret.GraphicsDeviceName);

            return ret;
        }
    }
}
