using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.LobbyServer.LobbyQueue
{
    public class LobbyQueueExceptions
    {
        public class MissingDefaultSubType : Exception { }
        public class LobbyQueueNotFoundException : Exception { }
    }
}
