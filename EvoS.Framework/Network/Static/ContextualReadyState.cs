using System;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(720)]
    public struct ContextualReadyState
    {
        public ReadyState ReadyState;
        public string GameProcessCode;
    }
}
