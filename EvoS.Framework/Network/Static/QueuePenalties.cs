using System;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(601)]
    public class QueuePenalties
    {
        public void ResetQueueDodge()
        {
            this.QueueDodgeCount = 0;
            this.QueueDodgeBlockTimeout = DateTime.MinValue;
            this.QueueDodgeParoleTimeout = DateTime.MinValue;
        }

        public int QueueDodgeCount;
        public DateTime QueueDodgeBlockTimeout;
        public DateTime QueueDodgeParoleTimeout;
    }
}
