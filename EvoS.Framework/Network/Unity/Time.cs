using System;

namespace EvoS.Framework.Network.Unity
{
    public class Time
    {
        private static float _startTime = time;
        
        public static float realtimeSinceStartup => time - _startTime;

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
