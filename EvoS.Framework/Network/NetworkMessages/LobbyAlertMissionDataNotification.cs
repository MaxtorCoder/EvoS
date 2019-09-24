using System;
using System.Collections.Generic;
using EvoS.Framework.Network.Static;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(14)]
    public class LobbyAlertMissionDataNotification : WebSocketMessage
    {
        public bool AlertMissionsEnabled;
        public ActiveAlertMission CurrentAlert;
        public DateTime? NextAlert;
        [EvosMessage(15)]
        public List<float> ReminderHours;
    }
}
