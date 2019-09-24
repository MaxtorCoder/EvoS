namespace EvoS.Framework.Network.Static
{
    public interface IPersistedGameplayStat
    {
        float GetSum();
        float GetMin();
        float GetMax();
        int GetNumGames();
        float Average();
    }
}
