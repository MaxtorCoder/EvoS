using EvoS.Framework.Network.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(460)]
    public class PurchaseLoadoutSlotRequest : WebSocketMessage
    {
        public CharacterType Character;
    }
}
