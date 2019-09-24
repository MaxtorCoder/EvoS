using System;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(172)]
    public struct TierDefinitions
    {
        public LocalizationPayload NameLocalization;
        public string IconResource;
        public bool IsRachet;
    }
}
