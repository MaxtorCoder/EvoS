using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

/*
 * Request sent by the player when it purchases a skin color
 */

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(303)]
    public class PurchaseTintRequest : WebSocketMessage
    {
        public CurrencyType CurrencyType;
        public CharacterType CharacterType;
        public int SkinId;
        public int TextureId;
        public int TintId;
    }
}
