using EvoS.Framework.Network;

namespace EvoS.Framework.Constants.Enums
{
    [EvosMessage(166)]
    public enum FreelancerDuplicationRuleTypes
    {
        byGameType,
        noneInGame,
        noneInTeam,
        allow,
        alwaysDupAcrossTeam,
        alwaysDupAcrossGame
    }
}
