using System.Collections.Generic;
using EvoS.Framework.Network.Game.Messages;

namespace EvoS.Framework.Game
{
    public class GameManagerHolder
    {
        private static Dictionary<string, GameManager> _gameManagers = new Dictionary<string, GameManager>();
        
        public static GameManager? FindGameManager(LoginRequest loginRequest)
        {
            // TODO this is only suitable for solo
            if (!_gameManagers.ContainsKey(loginRequest.SessionToken))
            {
                _gameManagers.Add(loginRequest.SessionToken, new GameManager());
            }

            return _gameManagers[loginRequest.SessionToken];
        }
    }
}
