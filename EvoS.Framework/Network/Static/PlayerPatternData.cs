using System;
using System.Collections.Generic;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(536)]
    public class PlayerPatternData
    {
        public PlayerPatternData()
        {
            Colors = new List<PlayerColorData>();
        }

        [EvosMessage(537)]
        public List<PlayerColorData> Colors { get; set; }

        public PlayerColorData GetColor(int i)
        {
            while (Colors.Count <= i)
            {
                Colors.Add(new PlayerColorData());
            }

            return Colors[i];
        }

        public PlayerPatternData GetDeepCopy()
        {
            PlayerPatternData playerPatternData = new PlayerPatternData();
            playerPatternData.Unlocked = Unlocked;
            foreach (PlayerColorData playerColorData in Colors)
            {
                playerPatternData.Colors.Add(playerColorData.GetDeepCopy());
            }

            return playerPatternData;
        }

        public bool Unlocked;
    }
}
