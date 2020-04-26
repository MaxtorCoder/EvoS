using EvoS.Framework.Network.Static;
using System;
using System.Collections.Generic;
using System.Text;

namespace CentralServer.LobbyServer.Account
{
    class Bank
    {
        public static BankComponent GetBankComponent(long accountId)
        {
            // TODO
            BankComponent bank = new BankComponent()
            {
                CurrentAmounts = new CurrencyWallet()
                {
                    Data = new List<CurrencyData>()
                    {
                        new CurrencyData() { Type = CurrencyType.ISO, Amount = Config.ConfigManager.DefaultISOAmount },
                        new CurrencyData() { Type = CurrencyType.FreelancerCurrency, Amount = Config.ConfigManager.DefaultFluxAmount },
                        new CurrencyData() { Type = CurrencyType.GGPack, Amount = Config.ConfigManager.DefaultGGAmount },
                        new CurrencyData() { Type = CurrencyType.RankedCurrency, Amount = Config.ConfigManager.DefaultRankedCurrencyAmount }
                    }
                }
            };

            return bank;
        }
    }
}
