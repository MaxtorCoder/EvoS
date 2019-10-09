using EvoS.Framework.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvoS.Framework.Constants.Enums
{
    [EvosMessage(275)]
    public enum PurchaseResult
    {
        Failed,
        Processing,
        Success
    }
}
