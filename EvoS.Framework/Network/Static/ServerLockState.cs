using System;
using EvoS.Framework.Network;

// Token: 0x02000783 RID: 1923
[EvosMessage(771)]
public enum ServerLockState
{
    Unknown,
    Unlocked,
    Locked,
    AutoLocked
}
