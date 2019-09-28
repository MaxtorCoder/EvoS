using System;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(717)]
    public class LobbyPlayerInfoUpdate
    {
        public int PlayerId;
        [EvosMessage(719)]
        public ContextualReadyState? ContextualReadyState;
        [EvosMessage(721)]
        public CharacterType? CharacterType;
        [EvosMessage(722)]
        public CharacterVisualInfo? CharacterSkin;
        [EvosMessage(726)]
        public CharacterCardInfo? CharacterCards;
        [EvosMessage(723)]
        public CharacterModInfo? CharacterMods;
        [EvosMessage(727)]
        public CharacterAbilityVfxSwapInfo? CharacterAbilityVfxSwaps;
        [EvosMessage(724)]
        public CharacterLoadoutUpdate? CharacterLoadoutChanges;
        public int? LastSelectedLoadout;
        [EvosMessage(718)]
        public BotDifficulty? AllyDifficulty;
        public BotDifficulty? EnemyDifficulty;
        public bool RankedLoadoutMods;
    }
}
