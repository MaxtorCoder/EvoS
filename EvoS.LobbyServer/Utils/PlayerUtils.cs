using EvoS.Framework.Network.Static;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.LobbyServer.Utils
{
    public class PlayerUtils
    {
        public static BankComponent GetBankData(long AccountID)
        {
            List<CurrencyData> currentBalances = new List<CurrencyData>
            {
                new CurrencyData() { m_Type = CurrencyType.ISO, Amount = 100000 },
                new CurrencyData() { m_Type = CurrencyType.FreelancerCurrency, Amount = 200000 },
                new CurrencyData() { m_Type = CurrencyType.GGPack, Amount = 999 },
                new CurrencyData() { m_Type = CurrencyType.RankedCurrency, Amount = 300 }
            };
            return new BankComponent(currentBalances);
        }
    }
}
