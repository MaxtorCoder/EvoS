using System;
using System.Collections.Generic;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(165)]
    public class GameMapConfig
    {
        public GameMapConfig Clone()
        {
            return (GameMapConfig) base.MemberwiseClone();
        }

        public GameMapConfig() {
        }

        public GameMapConfig(String mapName) {
            Map = mapName;
            IsActive = true;
        }

        public GameMapConfig(String mapName, bool isActive) {
            Map = mapName;
            IsActive = isActive;
        }

        public bool IsActive;
        public string Map;

        public static List<GameMapConfig> GetDeatmatchMaps()
        {
            return new List<GameMapConfig>
            {
                // TODO : uncomment later, some maps fail to load so ill use only one for debugging
                //new GameMapConfig(Maps.CargoShip_Deathmatch),
                //new GameMapConfig(Maps.Casino01_Deathmatch),
                //new GameMapConfig(Maps.EvosLab_Deathmatch),
                //new GameMapConfig(Maps.Oblivion_Deathmatch),
                //new GameMapConfig(Maps.Reactor_Deathmatch),
                //new GameMapConfig(Maps.RobotFactory_Deathmatch),
                new GameMapConfig(Maps.Skyway_Deathmatch),
            };
        }
    }
}
