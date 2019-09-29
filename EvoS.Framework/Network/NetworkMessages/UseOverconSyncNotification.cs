using System;
using System.Collections.Generic;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(407)]
    public class UseOverconSyncNotification : SyncNotification
    {
        public int OverconId;
        public long SenderAccountId;
        public int ActorId;
        public List<string> FellowRecipientHandles;
    }
}
