using System;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(215)]
    public class InventoryItem : ICloneable
    {
        public InventoryItem()
        {
            Id = 0;
            TemplateId = 0;
            Count = 0;
        }

        public InventoryItem(int templateId, int count = 1, int id = 0)
        {
            Id = id;
            TemplateId = templateId;
            Count = count;
        }

        public InventoryItem(InventoryItem itemToCopy, int id = 0)
        {
            Id = id;
            TemplateId = itemToCopy.TemplateId;
            Count = itemToCopy.Count;
        }

        public override string ToString()
        {
            return $"[{Id}] ItemTemplateId {TemplateId}, Count {Count}";
        }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public int Id;
        public int TemplateId;
        public int Count;
    }
}
