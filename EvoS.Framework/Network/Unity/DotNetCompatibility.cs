using System;
using System.Net.Sockets;

namespace EvoS.Framework.Network.Unity
{
    public static class DotNetCompatibility
    {
        public static string GetMethodName(this Delegate func)
        {
            return func.Method.Name;
        }

        public static Type GetBaseType(this Type type)
        {
            return type.BaseType;
        }

        public static string GetErrorCode(this SocketException e)
        {
            return e.ErrorCode.ToString();
        }
    }
}
