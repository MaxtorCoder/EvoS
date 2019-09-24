using System;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(339)]
    public class GamePackPriceOverride
    {
        public string productCode;
        public CountryPrices prices;
    }
}
