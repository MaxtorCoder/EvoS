using System.Collections.Generic;

namespace EvoS.Framework.Network.Static
{
    public abstract class ELOKeyComponent
    {
        public abstract KeyModeEnum KeyMode { get; }

        public abstract BinaryModePhaseEnum BinaryModePhase { get; }

        public abstract char GetComponentChar();

        public abstract char GetPhaseChar();

        public abstract string GetPhaseDescription();

        public abstract void Initialize(BinaryModePhaseEnum phase, GameType gameType, bool isCasual);

        public abstract void Initialize(List<MatchmakingQueueConfig.EloKeyFlags> flags, GameType gameType,
            bool isCasual);

        public abstract void InitializePerCharacter(byte groupSize);

        public abstract bool MatchesFlag(MatchmakingQueueConfig.EloKeyFlags flag);

        public enum KeyModeEnum
        {
            BINARY,
            SPECIFICSvsGENERAL
        }

        public enum BinaryModePhaseEnum
        {
            PRIMARY,
            SECONDARY,
            TERTIARY
        }
    }
}
