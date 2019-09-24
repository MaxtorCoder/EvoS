using EvoS.Framework.Network;

namespace EvoS.Framework.Constants.Enums
{
    [EvosMessage(45)]
    public enum PersistedStatBucket
    {
        None,
        DoNotPersist,
        NonCompetitive,
        Deathmatch_Unranked,
        Deathmatch_Ranked,
        BriefcaseExtraction,
        GameModes
    }
}
