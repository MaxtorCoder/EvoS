using System;
using System.Collections.Generic;
using System.Linq;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(495)]
    public class Loot
    {
        public Loot()
        {
            Karmas = new Dictionary<int, Karma>();
            Items = new List<InventoryItem>();
        }

        public void AddItem(InventoryItem item)
        {
            Items.Add(item);
        }

        public void AddItems(List<InventoryItem> items)
        {
            Items.AddRange(items);
        }

        public bool HasItem(int itemTemplateId) => Items.Exists(i => i.TemplateId == itemTemplateId);

        public IEnumerable<int> GetItemTemplateIds()
        {
            return from i in Items
                select i.TemplateId;
        }

        public void AddKarma(Karma karma)
        {
            if (Karmas.ContainsKey(karma.TemplateId))
            {
                Karmas[karma.TemplateId].Quantity += karma.Quantity;
            }
            else
            {
                Karmas[karma.TemplateId] = new Karma
                {
                    TemplateId = karma.TemplateId,
                    Quantity = karma.Quantity
                };
            }
        }

        public Karma GetKarma(int karmaTemplateId)
        {
            Karma result;
            Karmas.TryGetValue(karmaTemplateId, out result);
            return result;
        }

        public int GetKarmaQuantity(int karmaTemplateId)
        {
            int result = 0;
            Karma karma = GetKarma(karmaTemplateId);
            if (karma != null)
            {
                result = karma.Quantity;
            }

            return result;
        }

        public void MergeItems(Loot loot)
        {
            AddItems(loot.Items);
        }

        public List<InventoryItem> Items;

        public Dictionary<int, Karma> Karmas;
    }
}
