using System;

namespace EvoS.Framework.Network.Unity
{
    public class Time
    {
        public static float time
        {
            get
            {
                TimeSpan t = (DateTime.UtcNow - DateTime.UnixEpoch);
                return (float) t.TotalSeconds;
            }
        }
    }
}
