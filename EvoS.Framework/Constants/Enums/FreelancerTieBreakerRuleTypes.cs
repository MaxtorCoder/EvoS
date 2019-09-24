using EvoS.Framework.Network;

namespace EvoS.Framework.Constants.Enums
{
    [EvosMessage(133)]
    public enum FreelancerTieBreakerRuleTypes
    {
        random,
        lowestMMR,
        highestMMR,
        leastPlayed,
        mostPlayed
    }
}
