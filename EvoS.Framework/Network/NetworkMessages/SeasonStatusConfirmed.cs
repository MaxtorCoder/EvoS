using System;
using EvoS.Framework.Network.WebSocket;

namespace EvoS.Framework.Network.NetworkMessages
{
    [Serializable]
    [EvosMessage(440)]
    public class SeasonStatusConfirmed : WebSocketResponseMessage
    {
        public DialogType Dialog;

        [EvosMessage(441)]
        public enum DialogType
        {
            Unicode001D,
            Unicode000E
        }
    }
}
