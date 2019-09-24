using System;
using System.Collections.Generic;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(324)]
    public class CountryPrices
    {
        public float GetPrice(CurrencyCode currencyCode)
        {
            int num;
            if (Prices != null)
            {
                num = Prices.Length;
            }
            else
            {
                num = 0;
            }
            for (int i = 0; i < num; i++)
            {
                if (Prices[i].Currency == currencyCode)
                {
                    return Prices[i].Price;
                }
            }
            return 0f;
        }

        public override bool Equals(object obj)
        {
            CountryPrices countryPrices = obj as CountryPrices;
            if (countryPrices == null)
            {
                return false;
            }
            List<CountryPrice> list = new List<CountryPrice>();
            List<CountryPrice> list2 = new List<CountryPrice>();
            list.AddRange(Prices);
            list2.AddRange(countryPrices.Prices);
            for (int i = list.Count - 1; i >= 0; i--)
            {
                bool flag = false;
                for (int j = list2.Count - 1; j >= 0; j--)
                {
                    if (list[i].Equals(list2[j]))
                    {
                        list.RemoveAt(i);
                        list2.RemoveAt(j);
                        flag = true;
                        break;
                    }
                }
                if (!flag && list[i].Price > 0f)
                {
                    return false;
                }
            }
            bool flag2 = false;
            for (int k = 0; k < list.Count; k++)
            {
                if (list[k].Price > 0f)
                {
                    flag2 = true;
                    break;
                }
            }
            for (int l = 0; l < list2.Count; l++)
            {
                if (list2[l].Price > 0f)
                {
                    flag2 = true;
                    break;
                }
            }
            return !flag2;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        [EvosMessage(325)]
        public CountryPrice[] Prices;
    }

}
