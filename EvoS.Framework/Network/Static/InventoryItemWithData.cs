using System;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(214)]
    public class InventoryItemWithData
    {
        public InventoryItemWithData(InventoryItem item, int isoGained)
        {
            Item = item;
            IsoGained = isoGained;
        }

        public InventoryItem Item;
        public int IsoGained;
    }

}
