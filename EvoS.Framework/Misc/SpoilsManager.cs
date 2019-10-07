using System.Collections.Generic;
using EvoS.Framework.Network.Unity;

namespace EvoS.Framework.Misc
{
    public class SpoilsManager : MonoBehaviour
    {
        public PowerUp[] m_heroSpoils;
        public PowerUp[] m_minionSpoils;
        private List<PowerUp> m_activePowerUps;
        private static SpoilsManager s_instance;

        public enum SpoilsType
        {
            None,
            Hero,
            Minion,
        }
    }
}
