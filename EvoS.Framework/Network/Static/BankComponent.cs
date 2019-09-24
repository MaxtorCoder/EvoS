using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(589)]
    public class BankComponent
    {
        public BankComponent()
        {
            this.CurrentAmounts = new CurrencyWallet();
            this.Transactions = new Queue<CurrencyTransaction>();
        }

        public BankComponent(List<CurrencyData> currentBalances)
        {
            this.CurrentAmounts = new CurrencyWallet(currentBalances);
            this.Transactions = new Queue<CurrencyTransaction>();
        }

        public CurrencyWallet CurrentAmounts { get; set; }

        
        [EvosMessage(590)]
        public Queue<CurrencyTransaction> Transactions { get; set; }

        public int GetCurrentAmount(CurrencyType currencyType)
        {
            return this.CurrentAmounts.GetCurrentAmount(currencyType);
        }

        public bool CanAfford(CurrencyData cost)
        {
            return this.CurrentAmounts.CanAfford(cost);
        }

        public virtual void SetValue(CurrencyData newBalance)
        {
            this.CurrentAmounts.SetValue(newBalance);
        }

        public CurrencyData ChangeValue(CurrencyType currencyType, int amount, string source)
        {
            CurrencyData currencyData = this.CurrentAmounts.ChangeValue(currencyType, amount);
            if (currencyData == null)
            {
                return null;
            }

            this.Transactions.Enqueue(new CurrencyTransaction
            {
                Type = currencyType,
                Amount = amount,
                Source = source,
                Time = DateTime.UtcNow
            });
            while (this.Transactions.Count > 20)
            {
                this.Transactions.Dequeue();
            }

            return currencyData;
        }

        public BankComponent Clone()
        {
            string value = JsonConvert.SerializeObject(this);
            return JsonConvert.DeserializeObject<BankComponent>(value);
        }

        public const int MAX_SAVED_TRANSACTIONS = 20;
    }
}
