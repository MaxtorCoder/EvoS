using System;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Misc
{
    [Serializable]
    public struct TechPointInteraction
    {
        public TechPointInteractionType Type;
        public int Amount;
    }
}
