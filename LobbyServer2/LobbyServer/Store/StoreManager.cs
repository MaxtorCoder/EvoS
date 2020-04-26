using EvoS.Framework.Network.NetworkMessages;
using System;
using System.Collections.Generic;
using System.Text;

namespace CentralServer.LobbyServer.Store
{
    class StoreManager
    {
        public static PricesResponse GetPricesResponse()
        {
            return new PricesResponse()
            {
                characterPrices = new List<EvoS.Framework.Network.Static.CharacterPriceOverride>(),
                gamePackPrices = new List<EvoS.Framework.Network.Static.GamePackPriceOverride>(),
                ggPackPrices = new List<EvoS.Framework.Network.Static.GGPackPriceOverride>(),
                lootMatrixPackPrices = new List<LootMatrixPackPriceOverride>(),
                storeItemPrices = new List<EvoS.Framework.Network.Static.StoreItemPriceOverride>(),
                stylePrices = new List<EvoS.Framework.Network.Static.StylePriceOverride>()
            };
        }
    }
}
