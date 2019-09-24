using System;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(592)]
    public class CurrencyTransaction : CurrencyData
    {
        public string Source { get; set; }
        public DateTime Time { get; set; }
    }
}
