using EvoS.Framework.Network.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(459)]
    public class PurchaseLoadoutSlotResponse : WebSocketResponseMessage
    {
        public CharacterType Character;
    }
}
