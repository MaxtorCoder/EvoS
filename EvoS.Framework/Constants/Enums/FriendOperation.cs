using EvoS.Framework.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Framework.Constants.Enums
{
    [Serializable]
    [EvosMessage(363)]
    public enum FriendOperation
    {
        Unknown,
        Add,
        Remove,
        Accept,
        Reject,
        Block,
        Unblock,
        Note
    }
}
