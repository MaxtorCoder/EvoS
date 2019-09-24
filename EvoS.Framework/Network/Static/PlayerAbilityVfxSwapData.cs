using System;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(550)]
    public struct PlayerAbilityVfxSwapData
    {
        public override string ToString() => $"Ability[{AbilityId}]->VfxSwap[{AbilityVfxSwapID}]";

        public int AbilityId;

        public int AbilityVfxSwapID;
    }
}
