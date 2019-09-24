using System;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(499)]
    public class Karma
    {
        public bool IsValid() => this.Quantity >= 0;

        public int TemplateId;
        public int Quantity;
    }
}
