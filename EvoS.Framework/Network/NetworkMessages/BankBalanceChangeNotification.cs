using System;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(443)]
    public class BankBalanceChangeNotification : WebSocketResponseMessage
    {
        public CurrencyData NewBalance;
    }
}
