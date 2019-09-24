using System;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(330)]
    public class StoreItemPriceOverride
    {
        public string productCode;

        public CountryPrices prices;

        public int inventoryTemplateId;
    }
}
