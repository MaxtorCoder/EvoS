using System;
using EvoS.Framework.Network;

namespace EvoS.Framework.Constants.Enums
{
    [Flags]
    [EvosMessage(780)]
    public enum ProcessType
    {
        None = 0,
        DirectoryServer = 1,
        LobbyServer = 2,
        MatchmakingServer = 4,
        RelayServer = 8,
        MonitorServer = 16,
        LoadTestServer = 32,
        ReactorConsole = 1024,
        AtlasReactor = 32768,
        AtlasReactorServer = 65536,
        AtlasReactorDev = 131072,
        All = -1
    }
}
