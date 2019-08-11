using System;

// Token: 0x02000786 RID: 1926
[Serializable]
public class ConnectionQueueInfo
{
	public ClientAccessLevel QueueType;
	public int QueuePosition;
	public int QueueSize;
	public int QueueMultiplier;
	public int QueueEstimatedSeconds;
}
