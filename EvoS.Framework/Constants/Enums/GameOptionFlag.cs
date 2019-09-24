using System;
using EvoS.Framework.Network;

namespace EvoS.Framework.Constants.Enums
{
    [Flags]
    [EvosMessage(482)]
    public enum GameOptionFlag
    {
        None = 0,
        AllowDuplicateCharacters = 1,
        AllowPausing = 2,
        SkipEndOfGameCheck = 4,
        ReplaceHumansWithBots = 8,
        FakeClientConnections = 16,
        FakeGameServer = 32,
        NoInputIdleDisconnect = 64,
        EnableTeamAIOutput = 128,
        AutoLaunch = 256
    }
}
