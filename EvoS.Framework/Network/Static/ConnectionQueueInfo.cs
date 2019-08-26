using System;

// Token: 0x02000786 RID: 1926
[Serializable]
public class ConnectionQueueInfo
{
    public int QueueEstimatedSeconds;
    public int QueueMultiplier;
    public int QueuePosition;
    public int QueueSize;
    public ClientAccessLevel QueueType;
}
