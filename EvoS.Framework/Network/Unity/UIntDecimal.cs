using System;
using System.Runtime.InteropServices;

namespace EvoS.Framework.Network.Unity
{
    [StructLayout(LayoutKind.Explicit)]
    internal struct UIntDecimal
    {
        [FieldOffset(0)] public ulong longValue1;
        [FieldOffset(8)] public ulong longValue2;
        [FieldOffset(0)] public Decimal decimalValue;
    }
}
