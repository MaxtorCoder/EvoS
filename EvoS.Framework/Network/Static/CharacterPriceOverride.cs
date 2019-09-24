using System;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(342)]
    public class CharacterPriceOverride
    {
        public CharacterType characterType;
        public string productCode;
        public CountryPrices prices;
    }
}
