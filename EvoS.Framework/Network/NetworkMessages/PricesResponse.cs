using System;
using System.Collections.Generic;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(320)]
    public class PricesResponse : WebSocketResponseMessage
    {
        [EvosMessage(337)]
        public List<GamePackPriceOverride> gamePackPrices;
        [EvosMessage(331)]
        public List<LootMatrixPackPriceOverride> lootMatrixPackPrices;
        [EvosMessage(340)]
        public List<CharacterPriceOverride> characterPrices;
        [EvosMessage(334)]
        public List<GGPackPriceOverride> ggPackPrices;
        [EvosMessage(321)]
        public List<StylePriceOverride> stylePrices;
        [EvosMessage(328)]
        public List<StoreItemPriceOverride> storeItemPrices;
        public DateTime PacificTimeWithServerTimeOffset;
    }
}
