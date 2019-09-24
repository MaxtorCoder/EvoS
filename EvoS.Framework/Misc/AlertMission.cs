using System;
using EvoS.Framework.Network.Static;

namespace EvoS.Framework.Misc
{
    [Serializable]
    public class AlertMission : AlertMissionBase
    {
        public AlertMission Clone()
        {
            return (AlertMission) base.MemberwiseClone();
        }

        public QuestPrerequisites Prerequisites;
        public bool Enabled;
    }
}
