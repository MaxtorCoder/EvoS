using System;
using Newtonsoft.Json;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(444)]
    public class CurrencyData
    {
        public CurrencyData()
        {
            m_Type = CurrencyType.ISO;
            m_Amount = 10000;
            m_TotalSpent = 0;
        }

        public CurrencyType Type
        {
            get => m_Type;
            set => m_Type = value;
        }

        public int Amount
        {
            get => m_Amount;
            set => m_Amount = value;
        }

        public int TotalSpent
        {
            get => m_TotalSpent;
            set => m_TotalSpent = value;
        }

        public override string ToString()
        {
            return $"{m_Type.ToString()}: {m_Amount:N0}";
        }

        [JsonIgnore] public CurrencyType m_Type;
        [JsonIgnore] public int m_Amount;
        public int m_TotalSpent;
    }
}
