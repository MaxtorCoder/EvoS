using EvoS.Framework.Network;

namespace EvoS.Framework.Constants.Enums
{
    [EvosMessage(480)]
    public enum GameStatus
    {
        None,
        Assembling,
        FreelancerSelecting,
        LoadoutSelecting,
        Launching,
        Launched,
        Connecting,
        Connected,
        Authenticated,
        Loading,
        Loaded,
        Started,
        Stopped
    }
}
