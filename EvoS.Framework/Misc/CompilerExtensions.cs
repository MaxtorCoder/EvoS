using System.Collections.Generic;
using System.Linq;

namespace EvoS.Framework.Misc
{
    public static class CompilerExtensions
    {
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        public static bool IsNullOrEmpty<T>(this T[] t)
        {
            return t == null || t.Length == 0;
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> t)
        {
            return t == null || !t.Any<T>();
        }
    }
}
