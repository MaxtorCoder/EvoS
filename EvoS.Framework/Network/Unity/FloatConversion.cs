using System;

namespace EvoS.Framework.Network.Unity
{
    internal class FloatConversion
    {
        public static float ToSingle(uint value)
        {
            return new UIntFloat() {intValue = value}.floatValue;
        }

        public static double ToDouble(ulong value)
        {
            return new UIntFloat() {longValue = value}.doubleValue;
        }

        public static Decimal ToDecimal(ulong value1, ulong value2)
        {
            return new UIntDecimal()
            {
                longValue1 = value1,
                longValue2 = value2
            }.decimalValue;
        }
    }
}
