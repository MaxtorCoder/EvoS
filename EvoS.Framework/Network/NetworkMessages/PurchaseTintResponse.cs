using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

/*
 * Response sent by the server when the player purchases a skin color
 */

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(302)]
    public class PurchaseTintResponse : WebSocketResponseMessage
    {
        public PurchaseResult Result;
        public CurrencyType CurrencyType;
        public CharacterType CharacterType;
        public int SkinId;
        public int TextureId;
        public int TintId;
    }
}
