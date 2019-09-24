using System;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(323)]
    public class StylePriceOverride
    {
        public string productCode;
        public CharacterType characterType;
        public int skinIndex;
        public int textureIndex;
        public int colorIndex;
        public CountryPrices prices;
    }
}
