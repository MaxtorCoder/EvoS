using System;
using EvoS.Framework.Constants.Enums;
using Newtonsoft.Json;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(326)]
    public class CountryPrice
    {
        [JsonConstructor]
        public CountryPrice()
        {
            Currency = CurrencyCode.USD;
            Price = 0f;
        }

        public CountryPrice(CurrencyCode currency, float price)
        {
            Currency = currency;
            Price = price;
        }

        public override bool Equals(object obj)
        {
            CountryPrice countryPrice = obj as CountryPrice;
            return countryPrice != null && Currency == countryPrice.Currency && Price == countryPrice.Price;
        }

        public override int GetHashCode()
        {
            return (Currency + "|" + Price).GetHashCode();
        }

        public CurrencyCode Currency;

        public float Price;
    }
}
