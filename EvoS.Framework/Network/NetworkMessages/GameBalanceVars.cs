using System;

namespace EvoS.Framework.Network.NetworkMessages
{
    public class GameBalanceVars
    {
        [EvosMessage(146)]
        public enum GameRewardBucketType
        {
            FullVsHumanRewards,
            HumanVsBotsRewards,
            CasualGameRewards,
            NewPlayerRewards,
            PlacementMatchRewards,
            NoRewards
        }
    }
}
