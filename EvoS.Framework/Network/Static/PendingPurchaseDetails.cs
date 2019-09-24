using System;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(276)]
    public class PendingPurchaseDetails
    {
        public string Name => $"product {productCode} for player {purchaserName} ({channelTransactionId})";

        public string purchaserName;
        public Guid channelTransactionId;
        public PurchaseType purchaseType;
        public int[] typeSpecificData;
        public string productCode;
        public int quantity = 1;
        public int pollCount;
        public DateTime PricesRequestPacificTimeWithServerTimeOffsetAsOfPurchase;
    }
}
