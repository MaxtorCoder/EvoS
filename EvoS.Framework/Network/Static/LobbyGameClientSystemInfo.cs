using System;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(784, typeof(LobbyGameClientSystemInfo))]
    public class LobbyGameClientSystemInfo
    {
        public string GraphicsDeviceName;
    }
}
