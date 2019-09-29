using System;
using System.Collections.Generic;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(186)]
    public struct RankedResolutionPhaseData
    {
        [EvosMessage(187)]
        public List<RankedResolutionPlayerState> UnselectedPlayerStates;
        public List<RankedResolutionPlayerState> PlayersOnDeck;
        public TimeSpan TimeLeftInSubPhase;
        public List<CharacterType> FriendlyBans;
        public List<CharacterType> EnemyBans;
        [EvosMessage(195)]
        public Dictionary<int, CharacterType> FriendlyTeamSelections;
        public Dictionary<int, CharacterType> EnemyTeamSelections;
        [EvosMessage(191)]
        public List<RankedTradeData> TradeActions;
        public List<int> PlayerIdByImporance;
    }

}
