using System;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(165)]
    public class GameMapConfig
    {
        public GameMapConfig Clone()
        {
            return (GameMapConfig) base.MemberwiseClone();
        }

        public bool IsActive;
        public string Map;
    }
}
