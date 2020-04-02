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

        // Client: show a "loading" text in character selection
        // Server: started loading assets
        Launching,

        // Client: show the match loading screen
        // Server: finished loading assets
        Launched,
        Connecting,
        Connected,
        Authenticated,

        // Server: Some players stil loading
        Loading,

        // Server: All players loaded
        Loaded,

        // Server: Match Started
        Started,
        Stopped
    }
}
