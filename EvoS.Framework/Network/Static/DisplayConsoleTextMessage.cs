using System;
using EvoS.Framework.Constants.Enums;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    public struct DisplayConsoleTextMessage
    {
        public string Term;
        public string Context;
        public string Token;
        public string Unlocalized;
        public ConsoleMessageType MessageType;
        public Team RestrictVisibiltyToTeam;
        public string SenderHandle;
        public CharacterType CharacterType;
    }
}
