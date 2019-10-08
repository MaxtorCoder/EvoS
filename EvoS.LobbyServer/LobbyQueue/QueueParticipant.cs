using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.LobbyServer.LobbyQueue
{
    public abstract class QueueParticipant
    {
        public abstract bool IsGroup();
        public bool IsOnGroup;
        public DateTime time = new DateTime();

        public long GetSecondsQueued()
        {
            DateTime now = new DateTime();
            long time1 = now.Ticks / 10000000;
            long time2 = time.Ticks / 10000000;
            return time1 - time2;
        }
    }
}
