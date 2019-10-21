using System.Collections.Generic;
using EvoS.Framework.Network.Game.Messages;
using EvoS.Framework.Network.Static;

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
                var x = _gameManagers[loginRequest.SessionToken];
                x.SetTeamPlayerInfo(new List<LobbyPlayerInfo>
                {
                    new LobbyPlayerInfo()
                });
                x.SetGameInfo(new LobbyGameInfo
                {
                    GameConfig = new LobbyGameConfig
                    {
                        Map = "VR_Practice"
//                        Map = "CargoShip_Deathmatch"
//                        Map = "Casino01_Deathmatch"
//                        Map = "EvosLab_Deathmatch"
//                        Map = "Oblivion_Deathmatch"
//                        Map = "Reactor_Deathmatch"
//                        Map = "RobotFactory_Deathmatch"
//                        Map = "Skyway_Deathmatch"
                    }
                });
                x.LaunchGame();
            }

            return _gameManagers[loginRequest.SessionToken];
        }
    }
}
