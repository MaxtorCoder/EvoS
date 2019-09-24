using System;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(336)]
    public class GGPackPriceOverride
    {
        public string productCode;
        public CountryPrices prices;
    }
}
