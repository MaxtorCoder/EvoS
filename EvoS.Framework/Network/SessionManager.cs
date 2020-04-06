using EvoS.Framework.Constants.Enums;
using EvoS.Framework.Network.Static;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EvoS.Framework.Network
{
    public static class SessionManager
    {
        private static Dictionary<long, SessionPlayerInfo> PlayerInfoBySessionToken;
        private static int SessionTokenCounter = 0;


        public static long CreateSessionToken()
        {
            if (PlayerInfoBySessionToken == null) {
                PlayerInfoBySessionToken = new Dictionary<long, SessionPlayerInfo>();
            }
            return Interlocked.Increment(ref SessionTokenCounter);
        }

        public static SessionPlayerInfo Get(long sessionToken)
        {
            lock (PlayerInfoBySessionToken) {
                SessionPlayerInfo sessionPlayerInfo;
                PlayerInfoBySessionToken.TryGetValue(sessionToken, out sessionPlayerInfo);

                if (sessionPlayerInfo == null)
                {
                    sessionPlayerInfo = new SessionPlayerInfo { };
                    sessionPlayerInfo.SetCharacterType(CharacterType.Scoundrel);
                    sessionPlayerInfo.AllyBotDifficulty = BotDifficulty.Easy;
                    sessionPlayerInfo.EnemyBotDifficulty = BotDifficulty.Easy;

                    PlayerInfoBySessionToken.Add(sessionToken, sessionPlayerInfo);
                }

                return PlayerInfoBySessionToken[sessionToken];
            }
            
        }
    }
}
