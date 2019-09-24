namespace EvoS.Framework.Network.Static
{
    [EvosMessage(23)]
    public enum CurrencyType
    {
        ISO,
        ModToken,
        Credits,
        GGPack,
        Dust,
        Experience,
        RAFPoints,
        RankedCurrency,
        FreelancerCurrency,
        UnlockFreelancerToken,
        NONE = 1000
    }
}
