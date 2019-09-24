using System;
using EvoS.Framework.Misc;

namespace EvoS.Framework.Network.Static
{
    [Serializable]
    [EvosMessage(21)]
    public class ActiveAlertMission : AlertMissionBase
    {
        public ActiveAlertMission()
        {
        }

        public ActiveAlertMission(AlertMission alert)
        {
            base.CopyBaseVariables(alert);
        }

        public ActiveAlertMission Clone()
        {
            return (ActiveAlertMission) base.MemberwiseClone();
        }

        public DateTime StartTimePST;
    }
}
