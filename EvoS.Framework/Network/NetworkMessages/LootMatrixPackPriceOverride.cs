using System;
using EvoS.Framework.Network.Static;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(333)]
    public class LootMatrixPackPriceOverride
    {
        public string productCode;
        public CountryPrices prices;
    }
}
