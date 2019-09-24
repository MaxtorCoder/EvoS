using System;
using EvoS.Framework.Network;

[EvosMessage(714)]
public enum ClientAccessLevel
{
    Unknown,
    None,
    Locked = 10,
    Queued,
    Free = 20,
    Full = 22,
    const_6 = 25,
    Agent = 30,
    Admin = 40
}
