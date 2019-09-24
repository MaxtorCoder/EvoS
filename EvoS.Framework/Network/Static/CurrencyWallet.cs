using System;
using System.Collections;
using System.Collections.Generic;
using EvoS.Framework.Logging;
using Newtonsoft.Json;

namespace EvoS.Framework.Network.Static
{
    [JsonConverter(typeof(JsonConverter))]
    [Serializable]
    [EvosMessage(446)]
    public class CurrencyWallet : IEnumerable<CurrencyData>, IEnumerable
    {
        public CurrencyWallet()
        {
            Data = new List<CurrencyData>();
        }

        public CurrencyWallet(List<CurrencyData> data)
        {
            Data = data;
        }

        public CurrencyData this[int i] => Data[i];

        public int Count => Data.Count;

        public IEnumerator<CurrencyData> GetEnumerator() => Data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int GetCurrentAmount(CurrencyType currencyType) => GetValue(currencyType).Amount;

        public bool CanAfford(CurrencyData cost) => GetCurrentAmount(cost.Type) >= cost.Amount;

        public CurrencyData GetValue(CurrencyType currencyType)
        {
            for (int i = 0; i < Data.Count; i++)
            {
                if (Data[i].Type == currencyType)
                {
                    return Data[i];
                }
            }

            return new CurrencyData
            {
                Type = currencyType,
                Amount = 0
            };
        }

        public void SetValue(CurrencyData newBalance)
        {
            for (int i = 0; i < Data.Count; i++)
            {
                if (Data[i].Type == newBalance.Type)
                {
                    Data[i] = newBalance;
                    return;
                }
            }

            Data.Add(newBalance);
        }

        public CurrencyData ChangeValue(CurrencyType currencyType, int amount)
        {
            CurrencyData currencyData = null;
            for (int i = 0; i < Data.Count; i++)
            {
                if (Data[i].Type == currencyType)
                {
                    currencyData = Data[i];
                    break;
                }
            }

            if (currencyData == null)
            {
                currencyData = new CurrencyData
                {
                    Type = currencyType,
                    Amount = amount
                };
                Data.Add(currencyData);
            }
            else
            {
                int num = currencyData.Amount + amount;
                if (num < 0)
                {
                    Log.Print(LogType.Error,
                        $"Cannot withdraw {currencyType} amount {amount}, insufficient amount available.");
                    return null;
                }

                currencyData.Amount = num;
                if (amount < 0)
                {
                    currencyData.m_TotalSpent -= amount;
                }
            }

            return currencyData;
        }

        [EvosMessage(447)]
        public List<CurrencyData> Data;

        private class JsonConverter : Newtonsoft.Json.JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(CurrencyWallet);
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                CurrencyWallet currencyWallet = (CurrencyWallet) value;
                serializer.Serialize(writer, currencyWallet.Data);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
                JsonSerializer serializer)
            {
                CurrencyWallet currencyWallet;
                if (existingValue != null)
                {
                    currencyWallet = (CurrencyWallet) existingValue;
                }
                else
                {
                    currencyWallet = new CurrencyWallet();
                }

                CurrencyWallet currencyWallet2 = currencyWallet;
                serializer.Populate(reader, currencyWallet2.Data);
                return currencyWallet2;
            }
        }
    }
}
